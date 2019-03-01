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

        // GET: api/Patients
        [HttpGet]
        public IEnumerable<string> GetAll()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Patients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetByID(int id)
        {
            var patient = await _patientRepo.GetByID(id);
            patient.Emergency = await _emergencyRepo.GetByUserID(patient.UserID);
            patient.Provider = await _providerRepo.GetByID(patient.ProviderID);
            return patient;
        }

        // POST: api/Patients
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Patients/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
