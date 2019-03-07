using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalMentalityAPI.Models
{
    public class Appointment : InsertAppointment
    {
        public int ID { get; set; }
        public string ClinicianName { get; set; }
        public string ClinicianPicture { get; set; }
        public string PatientName { get; set; }
        public string PatientPicture { get; set; }
    }

    public class InsertAppointment
    {
        public Guid PatientID { get; set; }
        public int ClinicianID { get; set; }
        public DateTime Time { get; set; }
        public string Service { get; set; }
        public string Status { get; set; }
        public int Duration { get; set; }
    }
}
