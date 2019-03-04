using GlobalMentalityAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalMentalityAPI.Interfaces
{
    public interface IPatientRepository
    {
        Task<Patient> GetByID(Guid? id);
        Task<Guid?> InsertPatient(Patient patient);
        Task UpdatePatient(UpdatePatient patient);
    }
}
