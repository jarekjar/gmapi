using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GlobalMentalityAPI.Interfaces;
using GlobalMentalityAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobalMentalityAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientRepository _patientRepo;
        private readonly IEmergencyRepository _emergencyRepo;
        private readonly IClinicianRepository _clinicianRepo;
        private readonly IAppointmentRepository _appointmentRepo;

        public PatientsController(
            IPatientRepository patientRepo, 
            IEmergencyRepository emergencyRepo, 
            IClinicianRepository clinicianRepo,
            IAppointmentRepository appointmentRepo)
        {
            _patientRepo = patientRepo;
            _emergencyRepo = emergencyRepo;
            _clinicianRepo = clinicianRepo;
            _appointmentRepo = appointmentRepo;
        }

        /// <summary>
        /// Gets Patient info by ID.
        /// </summary>
        /// <param name="id"></param>  
        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetByID(Guid id)
        {
            var patient = await _patientRepo.GetByID(id);
            patient.Emergency = await _emergencyRepo.GetByUserID(patient.ID);
            patient.Clinician = await _clinicianRepo.GetByID(patient.ClinicianID);
            return patient;
        }

        /// <summary>
        /// Gets all patient appoint info by patient ID.
        /// </summary>
        /// <param name="id"></param>  
        [HttpGet("{id}/appointments")]
        public async Task<ActionResult<List<Appointment>>> GetAppointmentsByID(Guid id)
        {
            return await _appointmentRepo.GetByID(id);
        }
    }
}
