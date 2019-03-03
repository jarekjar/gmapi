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
    public class PatientRepository : IPatientRepository
    {
        private readonly IConfiguration _config;

        public PatientRepository(IConfiguration config)
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

        public async Task<Patient> GetByID(Guid id)
        {
            using (var con = mainConn)
            {
                string query = "SELECT * FROM dbo.Patients WHERE ID = @ID";
                var result = await con.QueryFirstOrDefaultAsync<Patient>(query, new { ID = id });
                return result;
            }
        }
    }
}
