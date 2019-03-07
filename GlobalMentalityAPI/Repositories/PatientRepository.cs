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

        public async Task UpdatePatient(UpdatePatient patient)
        {
            using (var con = mainConn)
            {
                string query = @"UPDATE [dbo].[Patients]
                                    SET
                                   UserID = IsNull(@UserID, UserID)
                                   ,FirstName = IsNull(@FirstName, FirstName)
                                   ,LastName = IsNull(@LastName, LastName)
                                   ,EmailAddress = IsNull(@EmailAddress, EmailAddress)
                                   ,Gender = IsNull(@Gender, Gender)
                                   ,DateOfBirth = IsNull(@DateOfBirth, DateOfBirth)
                                   ,Address = IsNull(@Address, Address)
                                   ,GroupInsuranceNumber = IsNull(@GroupInsuranceNumber, GroupInsuranceNumber)
                                   ,CellPhone = IsNull(@CellPhone, CellPhone)
                                   ,HomePhone = IsNull(@HomePhone, HomePhone)
                                   ,Picture = IsNull(@Picture, Picture)
                                   ,Insurance = IsNull(@Insurance, Insurance)
                                   WHERE ID = @ID";
                patient.DateOfBirth = Convert.ToDateTime(patient.DateOfBirth);
                await con.QueryAsync(query, patient);
                return;
            }
        }
    }
}
