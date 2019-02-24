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

            var Patients = new Patient[]
            {
                new Patient{FirstName="Carson",LastName="Alexander"},
                new Patient{FirstName="Meredith",LastName="Alonso"},
                new Patient{FirstName="Arturo",LastName="Anand"},
                new Patient{FirstName="Gytis",LastName="Barzdukas"},
                new Patient{FirstName="Yan",LastName="Li"},
                new Patient{FirstName="Peggy",LastName="Justice"},
                new Patient{FirstName="Laura",LastName="Norman"},
                new Patient{FirstName="Nino",LastName="Olivetto"}
            };

            foreach (Patient s in Patients)
            {
                context.Patients.Add(s);
            }

            context.SaveChanges();

            var courses = new Course[]
            {
                new Course{CourseID=1050,Title="Chemistry",Credits=3},
                new Course{CourseID=4022,Title="Microeconomics",Credits=3},
                new Course{CourseID=4041,Title="Macroeconomics",Credits=3},
                new Course{CourseID=1045,Title="Calculus",Credits=4},
                new Course{CourseID=3141,Title="Trigonometry",Credits=4},
                new Course{CourseID=2021,Title="Composition",Credits=3},
                new Course{CourseID=2042,Title="Literature",Credits=4}
            };
            foreach (Course c in courses)
            {
                context.Courses.Add(c);
            }
            context.SaveChanges();

            var enrollments = new Enrollment[]
            {
                new Enrollment{PatientID=1,CourseID=1050,Grade=Grade.A},
                new Enrollment{PatientID=1,CourseID=4022,Grade=Grade.C},
                new Enrollment{PatientID=1,CourseID=4041,Grade=Grade.B},
                new Enrollment{PatientID=2,CourseID=1045,Grade=Grade.B},
                new Enrollment{PatientID=2,CourseID=3141,Grade=Grade.F},
                new Enrollment{PatientID=2,CourseID=2021,Grade=Grade.F},
                new Enrollment{PatientID=3,CourseID=1050},
                new Enrollment{PatientID=4,CourseID=1050},
                new Enrollment{PatientID=4,CourseID=4022,Grade=Grade.F},
                new Enrollment{PatientID=5,CourseID=4041,Grade=Grade.C},
                new Enrollment{PatientID=6,CourseID=1045},
                new Enrollment{PatientID=7,CourseID=3141,Grade=Grade.A},
            };

            foreach (Enrollment e in enrollments)
            {
                context.Enrollments.Add(e);
            }

            context.SaveChanges();
        }
        
    }
}
