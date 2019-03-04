using GlobalMentalityAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalMentalityAPI.Interfaces
{
    public interface IUserRepository
    {
        Task<int> InsertUser(User user);
        Task<LoggedUser> Login(LoginUser user);
    }
}
