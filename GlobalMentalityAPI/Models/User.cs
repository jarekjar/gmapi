using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalMentalityAPI.Models
{
    public class User : InsertUser
    {
        public int ID { get; set; }
        public DateTime DateCreated { get; set; }
    }

    public class InsertUser : LoginUser
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Role { get; set; }
    }

    public class LoginUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoggedUser
    {
        public int ID { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
    }
}