using GlobalMentalityAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalMentalityAPI.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<List<Appointment>> GetByPatientID(Guid id);
        Task<List<Appointment>> GetByClinicianID(int id);
        Task<int> InsertAppointment(Appointment app);
        Task<int> UpdateAppointment(Appointment app);
    }
}
