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
            return await _appointmentRepo.GetByPatientID(id);
        }

        /// <summary>
        /// Inserts a new patient, generates a new ID.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Todo
        ///     {
        ///      "id": null,
        ///      "userID": 0,
        ///      "clinicianID": 1,
        ///      "firstName": "Jared",
        ///      "lastName": "Smith",
        ///      "cellPhone": "951-217-4555",
        ///      "homePhone": null,
        ///      "emailAddress": "jaredkjar@gmail.com",
        ///      "picture": "string",
        ///      "address": "9393 String St, Los Angeles CA 92345",
        ///      "gender": "Male",
        ///      "dateOfBirth": "2019-03-03T21:35:45.785Z",
        ///      "insurance": "Aetna",
        ///      "groupInsuranceNumber": 123452
        ///      }
        ///
        /// </remarks>
        /// <param name="patient"></param>
        /// <returns>Patiend ID</returns> 
        [HttpPost]
        [Authorize(Roles = Role.Clinician + "," + Role.OfficeAdmin + "," + Role.SuperAdmin)]
        public async Task<ActionResult<Guid>> InsertPatient (Patient patient)
        {
            return await _patientRepo.InsertPatient(patient);
        }

        /// <summary>
        /// Updates a patient.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Todo
        ///     {
        ///      "id": null,
        ///      "userID": 0,
        ///      "clinicianID": 1,
        ///      "firstName": "Jared",
        ///      "lastName": "Smith",
        ///      "cellPhone": "951-217-4555",
        ///      "homePhone": null,
        ///      "emailAddress": "jaredkjar@gmail.com",
        ///      "picture": "string",
        ///      "address": "9393 String St, Los Angeles CA 92345",
        ///      "gender": "Male",
        ///      "dateOfBirth": "2019-03-03T21:35:45.785Z",
        ///      "insurance": "Aetna",
        ///      "groupInsuranceNumber": 123452
        ///      }
        ///
        /// </remarks>
        /// <param name="patient"></param>
        /// <returns>Patiend ID</returns> 
        [HttpPut]
        [Authorize(Roles = Role.Clinician + "," + Role.OfficeAdmin + "," + Role.SuperAdmin)]
        public async Task<ActionResult<Patient>> UpdatePatient(Patient patient)
        {
            return await _patientRepo.UpdatePatient(patient);
        }
    }
}
