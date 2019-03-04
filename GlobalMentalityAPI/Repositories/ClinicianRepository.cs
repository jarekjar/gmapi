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

        public async Task<List<UpdatePatient>> GetPatientsByID(int id)
        {
            using (var con = mainConn)
            {
                string query = "SELECT * FROM dbo.Patients WHERE ClinicianID = @ID";
                var result = await con.QueryAsync<UpdatePatient>(query, new { ID = id });
                return result.ToList();
            }
        }

        public async Task<int> InsertClinician(InsertClinician clinician)
        {
            using (var con = mainConn)
            {
                string query = @"INSERT INTO [dbo].[Clinicians]
                                ([UserID]
                                ,[FirstName]
                                ,[LastName]
                                ,[PhoneNum]
                                ,[EmailAddress]
                                ,[Title]
                                ,[Address]
                                ,[Practice]
                                ,[FaxNum])
                            VALUES
                                (@UserID, @FirstName, @LastName, @PhoneNum, @EmailAddress,
                                @Title, @Address, @Practice, @FaxNum);
                            SELECT SCOPE_IDENTITY()";
                var id = await con.QueryFirstOrDefaultAsync<int>(query, clinician);
                return id;
            }
        }

        public async Task UpdateClinician(Clinician clinician)
        {
            using (var con = mainConn)
            {
                string query = @"UPDATE [dbo].[Clinicians]
                                    SET
                                [UserID] = @UserID
                                ,[FirstName] = @FirstName
                                ,[LastName] = @LastName
                                ,[PhoneNum] = @PhoneNum
                                ,[EmailAddress] = @EmailAddress
                                ,[Title] = @Title
                                ,[Address] = @Address
                                ,[Practice] = @Practice
                                ,[FaxNum] = @FaxNum
                                WHERE ID = @ID;";
                await con.QueryAsync<int>(query, clinician);
                return;
            }
        }

    }
}
