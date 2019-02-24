using GlobalMentalityAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalMentalityAPI.Data
{
    public static class DbInitializer
    {
        public static void Initialize(BusinessContext context)
        {
            context.Database.EnsureCreated();

            // Look for any Patients.
            if (context.Patients.Any())
            {
                return;   // DB has been seeded
            }

            var users = new User[]
            {
                new User{Username = "one"},
                new User{Username = "two"},
                new User{Username = "three"},
                new User{Username = "four"},
                new User{Username = "five"},
                new User{Username = "six"},
                new User{Username = "seven"},
                new User{Username = "eight"},
                new User{Username = "nine"},
                new User{Username = "ten"},
                new User{Username = "eleven"}
            };

            foreach (User u in users)
            {
                context.Users.Add(u);
            }

            context.SaveChanges();

            var businesses = new Business[]
            {
                new Business{ Name="The Great Business" }
            };

            foreach (Business b in businesses)
            {
                context.Businesses.Add(b);
            }

            context.SaveChanges();

            var doctors = new Doctor[]
            {
                new Doctor{LastName="Carson",FirstName="Alexander", UserID = users.Single(u => u.Username == "one").ID, BusinessID = 1} ,
                new Doctor{LastName="Meredith",FirstName="Alonso", UserID = users.Single(u => u.Username == "two").ID, BusinessID = 1 }
            };

            foreach (Doctor d in doctors)
            {
                context.Doctors.Add(d);
            }

            context.SaveChanges();

            var patients = new Patient[]
            {
                new Patient{FirstName="Carson",LastName="Alexander", UserID = users.Single(u => u.Username == "three").ID, DoctorID = 1, BusinessID = 1},
                new Patient{FirstName="Arturo",LastName="Anand", UserID = users.Single(u => u.Username == "four").ID, DoctorID = 1, BusinessID = 1},
                new Patient{FirstName="Meredith",LastName="Alonso", UserID = users.Single(u => u.Username == "five").ID, DoctorID = 1, BusinessID = 1},
                new Patient{FirstName="Gytis",LastName="Barzdukas", UserID = users.Single(u => u.Username == "six").ID, DoctorID = 1, BusinessID = 1},
                new Patient{FirstName="Yan",LastName="Li", UserID = users.Single(u => u.Username == "seven").ID, DoctorID = 1, BusinessID = 1},
                new Patient{FirstName="Peggy",LastName="Justice", UserID = users.Single(u => u.Username == "eight").ID, DoctorID = 2, BusinessID = 1},
                new Patient{FirstName="Laura",LastName="Norman", UserID = users.Single(u => u.Username == "nine").ID, DoctorID = 2, BusinessID = 1},
                new Patient{FirstName="Nino",LastName="Olivetto" , UserID = users.Single(u => u.Username == "ten").ID, DoctorID = 2, BusinessID = 1}
            };

            foreach (Patient p in patients)
            {
                context.Patients.Add(p);
            }

            context.SaveChanges();

            

            var admins = new OfficeAdmin[]
            {
                new OfficeAdmin{LastName="Jared",FirstName="Alexander", UserID = users.Single(u => u.Username == "eleven").ID, BusinessID = 1 }
            };

            foreach (OfficeAdmin a in admins)
            {
                context.OfficeAdmins.Add(a);
            }

            context.SaveChanges();
        }

    }
}
