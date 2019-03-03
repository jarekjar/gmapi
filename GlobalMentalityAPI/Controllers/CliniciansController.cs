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
    public class CliniciansController : ControllerBase
    {
        private readonly IPatientRepository _patientRepo;
        private readonly IEmergencyRepository _emergencyRepo;
        private readonly IClinicianRepository _clinicianRepo;

        public CliniciansController(IPatientRepository patientRepo, IEmergencyRepository emergencyRepo, IClinicianRepository clinicianRepo)
        {
            _patientRepo = patientRepo;
            _emergencyRepo = emergencyRepo;
            _clinicianRepo = clinicianRepo;
        }

        /// <summary>
        /// Gets Clinician info by ID.
        /// </summary>
        /// <param name="id"></param> 
        [HttpGet("{id}")]
        public async Task<Clinician> GetByID(int id)
        {
            return await _clinicianRepo.GetByID(id);
        }

        /// <summary>
        /// Gets list of clinicians for the doctor by id.
        /// </summary>
        /// <param name="id"></param> 
        [HttpGet("{id}/patients")]
        public async Task<List<Patient>> GetPatientsByID(int id)
        {
            return await _clinicianRepo.GetPatientsByID(id);
        }
    }
}
