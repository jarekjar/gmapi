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

        public async Task<Patient> GetByID(Guid? id)
        {
            using (var con = mainConn)
            {
                string query = "SELECT * FROM dbo.Patients WHERE ID = @ID";
                var result = await con.QueryFirstOrDefaultAsync<Patient>(query, new { ID = id });
                return result;
            }
        }

        public async Task<Guid?> InsertPatient(Patient patient)
        {
            using (var con = mainConn)
            {
                string query = @"INSERT INTO [dbo].[Patients]
                                   ([UserID]
                                   ,[ClinicianID]
                                   ,[FirstName]
                                   ,[LastName]
                                   ,[EmailAddress]
                                   ,[Gender]
                                   ,[DateOfBirth]
                                   ,[Address]
                                   ,[GroupInsuranceNumber]
                                   ,[CellPhone]
                                   ,[HomePhone]
                                   ,[Picture]
                                   ,[Insurance]
                                   ,[ID])
                             VALUES
                                (@UserID, @ClinicianID, @FirstName, @LastName, @EmailAddress,
                                @Gender, @DateOfBirth, @Address, @GroupInsuranceNumber, @CellPhone,
                                @HomePhone, @Picture, @Insurance, @ID)";
                patient.ID = Guid.NewGuid();
                await con.QueryAsync(query, patient);
                return patient.ID;
            }
        }

        public async Task<Patient> UpdatePatient(Patient patient)
        {
            using (var con = mainConn)
            {
                string query = @"UPDATE [dbo].[Patients]
                                    SET
                                   UserID = @UserID
                                   ,ClinicianID = @ClinicianID
                                   ,FirstName = @FirstName
                                   ,LastName = @LastName
                                   ,EmailAddress = @EmailAddress
                                   ,Gender = @Gender
                                   ,DateOfBirth = @DateOfBirth
                                   ,Address = @Address
                                   ,GroupInsuranceNumber = @GroupInsuranceNumber
                                   ,CellPhone = @CellPhone
                                   ,HomePhone = @HomePhone
                                   ,Picture = @Picture
                                   ,Insurance = @Insurance
                                   WHERE ID = @ID";
                await con.QueryAsync(query, patient);
                return patient;
            }
        }
    }
}
