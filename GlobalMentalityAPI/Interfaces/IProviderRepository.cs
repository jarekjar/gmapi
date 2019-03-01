using GlobalMentalityAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalMentalityAPI.Interfaces
{
    public interface IProviderRepository
    {
        Task<Provider> GetByID(int id);
    }
}
