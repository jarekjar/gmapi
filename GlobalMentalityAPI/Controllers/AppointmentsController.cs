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
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentRepository _appointmentRepo;
        private readonly IPatientRepository _patientRepo;
        private readonly IClinicianRepository _clinicianRepo;

        public AppointmentsController(IAppointmentRepository appointmentRepo, IClinicianRepository clinicianRepo, IPatientRepository patientRepo)
        {
            _appointmentRepo = appointmentRepo;
            _clinicianRepo = clinicianRepo;
            _patientRepo = patientRepo;
        }

        /// <summary>
        /// Inserts appointment, brings back appointment ID.
        /// </summary>
        /// <param name="app"></param>
        /// <returns>User Object</returns> 
        [HttpPost]
        public async Task<ActionResult<int>> InsertAppointment (InsertAppointment app)
        {
            var patientInfo = await _patientRepo.GetByID(app.PatientID);
            var clinicianInfo = await _clinicianRepo.GetByID(app.ClinicianID);
            var fullApp = (Appointment)app;
            fullApp.ClinicianName = clinicianInfo.FirstName + " " + clinicianInfo.LastName;
            fullApp.ClinicianPicture = "picture";
            fullApp.PatientName = patientInfo.FirstName + " " + patientInfo.LastName;
            fullApp.PatientPicture = "picture";
            return await _appointmentRepo.InsertAppointment(fullApp);
        }

        /// <summary>
        /// Updates appointment, brings back appointment ID.
        /// </summary>
        /// <param name="app"></param>
        /// <returns>User Object</returns> 
        [HttpPut]
        public async Task<ActionResult<int>> UpdateAppointment(Appointment app)
        {
            return await _appointmentRepo.UpdateAppointment(app);
        }
    }
}
