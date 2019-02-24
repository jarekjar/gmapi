using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalMentalityAPI.Models
{
    public class Patient
    {
        [Key]
        public int PatientID { get; set; }
        [ForeignKey("User")]
        public int UserID { get; set; }
        [ForeignKey("Business")]
        public int BusinessID { get; set; }
        public string FirstName { get; set; }
        [Phone]
        public string PhoneNum { get; set; }
        [EmailAddress]
        public string EmailAddress { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string InsuranceProvider { get; set; }
        public int GroupInsuranceNumber { get; set; }
        public int DoctorID { get; set; }
    }
}
