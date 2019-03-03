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
        private readonly IProviderRepository _providerRepo;

        public PatientsController(IPatientRepository patientRepo, IEmergencyRepository emergencyRepo, IProviderRepository providerRepo)
        {
            _patientRepo = patientRepo;
            _emergencyRepo = emergencyRepo;
            _providerRepo = providerRepo;
        }

        /// <summary>
        /// Deletes a specific TodoItem.
        /// </summary>
        /// <param name="id"></param>  
        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetByID(int id)
        {
            var patient = await _patientRepo.GetByID(id);
            patient.Emergency = await _emergencyRepo.GetByUserID(patient.UserID);
            patient.Provider = await _providerRepo.GetByID(patient.ProviderID);
            return patient;
        }
    }
}
