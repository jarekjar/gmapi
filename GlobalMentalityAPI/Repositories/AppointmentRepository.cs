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

        public async Task<int> InsertAppointment(Appointment app)
        {
            using (var con = mainConn)
            {
                string query = @"Insert into dbo.Appoints (
                                [PatientID],
                               [Time]
                               ,[ClinicianName]
                               ,[ClinicianPicture]
                               ,[PatientName]
                               ,[PatientPicture]
                               ,[Service]
                               ,[Status]
                               ,[Duration]
                               ,[ClinicianID]) 
                                values
                                (@PatientID
                                @Time, 
                                @ClinicianName, 
                                @ClinicianPicture, 
                                @PatientName, 
                                @PatientPicture, 
                                @Service, 
                                @Status, 
                                @Duration, 
                                @ClinicianID);
                                select scope_identity();"
                                ;
                var result = await con.QueryFirstOrDefaultAsync<int>(query, app);
                return result;
            }
        }

        public async Task<int> UpdateAppointment(Appointment app)
        {
            using (var con = mainConn)
            {
                string query = @"Update dbo.Appoints (
                                [PatientID = @PatientID]
                               [Time] = @Time
                               ,[ClinicianName] = @ClinicianName
                               ,[ClinicianPicture] = @ClinicianPicture
                               ,[PatientName] = @PatientName
                               ,[PatientPicture] = @PatientPicture
                               ,[Service] = @Service
                               ,[Status] = @Status
                               ,[Duration] = @Duration
                               ,[ClinicianID] =  @ClinicianID)
                                WHERE ID = @ID
                                select scope_identity();"
                                ;
                var result = await con.QueryFirstOrDefaultAsync<int>(query, app);
                return result;
            }
        }
    }
}
