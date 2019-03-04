using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GlobalMentalityAPI.Attributes;
using GlobalMentalityAPI.Interfaces;
using GlobalMentalityAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobalMentalityAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [ValidateModel]
    [Authorize]
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
            try
            {
                var patient = await _patientRepo.GetByID(id);
                patient.Emergency = await _emergencyRepo.GetByUserID(patient.ID);
                patient.Clinician = await _clinicianRepo.GetByID(patient.ClinicianID);
                return patient;
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }

        }

        /// <summary>
        /// Gets all patient appoint info by patient ID.
        /// </summary>
        /// <param name="id"></param>  
        [HttpGet("{id}/appointments")]
        public async Task<ActionResult<List<Appointment>>> GetAppointmentsByID(Guid id)
        {
            try
            {
                return await _appointmentRepo.GetByPatientID(id);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Inserts a new patient, generates a new ID.
        /// </summary>
        /// <param name="patient"></param>
        /// <returns>Patiend ID</returns> 
        [HttpPost]
        [Authorize(Roles = Role.Clinician + "," + Role.OfficeAdmin + "," + Role.SuperAdmin)]
        public async Task<ActionResult<Guid>> InsertPatient (InsertPatient patient)
        {
            try
            {
                return await _patientRepo.InsertPatient((Patient)patient);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Updates a patient.
        /// </summary>
        /// <param name="patient"></param>
        /// <returns>Ok!</returns> 
        [HttpPut]
        [Authorize(Roles = Role.Clinician + "," + Role.OfficeAdmin + "," + Role.SuperAdmin)]
        public async Task<ActionResult> UpdatePatient(UpdatePatient patient)
        {
            try
            {
                await _patientRepo.UpdatePatient(patient);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
