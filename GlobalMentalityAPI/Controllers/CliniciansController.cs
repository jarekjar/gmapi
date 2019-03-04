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
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CliniciansController : ControllerBase
    {
        private readonly IPatientRepository _patientRepo;
        private readonly IEmergencyRepository _emergencyRepo;
        private readonly IClinicianRepository _clinicianRepo;
        private readonly IAppointmentRepository _appointmentRepo;

        public CliniciansController(
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
        /// Gets Clinician info by ID.
        /// </summary>
        /// <param name="id"></param> 
        [HttpGet("{id}")]
        public async Task<ActionResult<Clinician>> GetByID(int id)
        {
            try
            {
                return await _clinicianRepo.GetByID(id);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Gets list of clinicians for the doctor by id.
        /// </summary>
        /// <param name="id"></param> 
        [HttpGet("{id}/patients")]
        [Authorize(Roles = Role.Clinician + "," + Role.OfficeAdmin + "," + Role.SuperAdmin)]
        public async Task<ActionResult<List<Patient>>> GetPatientsByID(int id)
        {
            try
            {
                return await _clinicianRepo.GetPatientsByID(id);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Gets all doctor appoint info by patient ID.
        /// </summary>
        /// <param name="id"></param>  
        [HttpGet("{id}/appointments")]
        [Authorize(Roles = Role.Clinician + "," + Role.OfficeAdmin + "," + Role.SuperAdmin)]
        public async Task<ActionResult<List<Appointment>>> GetAppointmentsByID(int id)
        {
            try
            {
                return await _appointmentRepo.GetByClinicianID(id);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Insert a new Doctor.
        /// </summary>
        /// <param name="clinician"></param> 
        [HttpPost]
        [Authorize(Roles = Role.OfficeAdmin + "," + Role.SuperAdmin)]
        public async Task<ActionResult<int>> InsertClinician(Clinician clinician)
        {
            try
            {
                return await _clinicianRepo.InsertClinician(clinician);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Updates a doctor.
        /// </summary>
        /// <param name="clinician"></param> 
        [HttpPut]
        [Authorize(Roles = Role.OfficeAdmin + "," + Role.SuperAdmin)]
        public async Task<ActionResult<Clinician>> UpdateClinician(Clinician clinician)
        {
            try
            {
                return await _clinicianRepo.UpdateClinician(clinician);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
