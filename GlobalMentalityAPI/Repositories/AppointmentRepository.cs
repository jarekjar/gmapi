using GlobalMentalityAPI.Interfaces;
using GlobalMentalityAPI.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace GlobalMentalityAPI.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly IConfiguration _config;

        public AppointmentRepository(IConfiguration config)
        {
            _config = config;
        }

        public IDbConnection mainConn
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        public async Task<List<Appointment>> GetByPatientID(Guid id)
        {
            using (var con = mainConn)
            {
                string query = "SELECT * FROM dbo.Appointments WHERE PatientID = @ID";
                var result = await con.QueryAsync<Appointment>(query, new { ID = id });
                return result.ToList();
            }
        }

        public async Task<List<Appointment>> GetByClinicianID(int id)
        {
            using (var con = mainConn)
            {
                string query = "SELECT * FROM dbo.Appointments WHERE ClinicianID = @ID";
                var result = await con.QueryAsync<Appointment>(query, new { ID = id });
                return result.ToList();
            }
        }
    }
}
