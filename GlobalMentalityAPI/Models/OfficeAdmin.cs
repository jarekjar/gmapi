using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalMentalityAPI.Models
{
    public class OfficeAdmin
    {
        [Key]
        public int AdminID { get; set; }
        [ForeignKey("User")]
        public int UserID { get; set; }
        [ForeignKey("Business")]
        public int BusinessID { get; set; }
        public string FirstName { get; set; }
        [Phone]
        public string PhoneNum { get; set; }
        [EmailAddress]
        public string EmailAddress { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
    }
}
