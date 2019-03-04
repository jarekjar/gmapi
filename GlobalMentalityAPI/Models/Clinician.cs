using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalMentalityAPI.Models
{
    public class Clinician : InsertClinician
    {
        public int ID { get; set; }
        
    }

    public class InsertClinician
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Practice { get; set; }
        [Phone]
        public string PhoneNum { get; set; }
        public string FaxNum { get; set; }
        [EmailAddress]
        public string EmailAddress { get; set; }
        public string Title { get; set; }
    }
}
