using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalMentalityAPI.Models
{
    public class OfficeAdmin : User
    {
        public string FirstName { get; set; }
        [Phone]
        public string PhoneNum { get; set; }
        [EmailAddress]
        public string EmailAddress { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
    }
}
