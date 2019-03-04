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
        public Guid? ID { get; set; }
        public int UserID { get; set; }
        [Required]
        public int ClinicianID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Phone]
        public string CellPhone { get; set; }
        [Phone]
        public string HomePhone { get; set; }
        [EmailAddress]
        public string EmailAddress { get; set; }
        public string Picture { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Insurance { get; set; }
        public int GroupInsuranceNumber { get; set; }
        public Clinician Clinician { get; set; }
        public Emergency Emergency { get; set; } 
        public int? EmergencyID { get; set; }
    }
    
    public class Emergency
    {
        public int ID { get; set; }
        public Guid PatientID { get; set; }
        public string Relationship { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string HomePhone { get; set; }
        public string CellPhone { get; set; }
    }
}
