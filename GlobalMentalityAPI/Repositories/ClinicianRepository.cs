using GlobalMentalityAPI.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using GlobalMentalityAPI.Models;

namespace GlobalMentalityAPI.Repositories
{
    public class ClinicianRepository : IClinicianRepository
    {
        private readonly IConfiguration _config;

        public ClinicianRepository(IConfiguration config)
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

        public async Task<Clinician> GetByID(int id)
        {
            using (var con = mainConn)
            {
                string query = "SELECT * FROM dbo.Clinicians WHERE ID = @ID";
                var result = await con.QueryFirstOrDefaultAsync<Clinician>(query, new { ID = id });
                return result;
            }
        }

        public async Task<List<Patient>> GetPatientsByID(int id)
        {
            using (var con = mainConn)
            {
                string query = "SELECT * FROM dbo.Patients WHERE ClinicianID = @ID";
                var result = await con.QueryAsync<Patient>(query, new { ID = id });
                return result.ToList();
            }
        }

    }
}
