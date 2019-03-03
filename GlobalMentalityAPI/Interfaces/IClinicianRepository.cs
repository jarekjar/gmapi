using GlobalMentalityAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalMentalityAPI.Interfaces
{
    public interface IClinicianRepository
    {
        Task<Clinician> GetByID(int id);
        Task<List<Patient>> GetPatientsByID(int id);
    }
}
