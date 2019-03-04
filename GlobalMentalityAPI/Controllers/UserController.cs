using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GlobalMentalityAPI.Attributes;
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
    [ValidateModel]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        /// <summary>
        /// Inserts a new user, brings back ID.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>User ID</returns> 
        [HttpPost]
        [Authorize(Roles = Role.Clinician + "," + Role.OfficeAdmin + "," + Role.SuperAdmin)]
        public async Task<ActionResult<int>> InsertUser(InsertUser user)
        {
            try
            {
                //cast InsertUser into User object.
                return await _userRepository.InsertUser((User)user);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Logs in user, brings back user data.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>User Object</returns> 
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<LoggedUser>> Login(LoginUser user)
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
