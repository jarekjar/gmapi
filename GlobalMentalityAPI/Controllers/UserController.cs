using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GlobalMentalityAPI.Interfaces;
using GlobalMentalityAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobalMentalityAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        
        [HttpPost]
        [Authorize(Roles = Role.Clinician + "," + Role.OfficeAdmin + "," + Role.SuperAdmin)]
        public async Task<ActionResult<int>> InsertUser(User user)
        {
            try
            {
                return await _userRepository.InsertUser(user);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(User user)
        {
            try
            {
                var loggedUser = await _userRepository.Login(user);

                if (loggedUser == null)
                    return BadRequest(new { message = "Username or password is incorrect" });

                return loggedUser;
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
