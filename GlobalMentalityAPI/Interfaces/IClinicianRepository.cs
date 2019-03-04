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
        Task<List<UpdatePatient>> GetPatientsByID(int id);
        Task<int> InsertClinician(InsertClinician clinician);
        Task UpdateClinician(Clinician clinician);
    }
}
