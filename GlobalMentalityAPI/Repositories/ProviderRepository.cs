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
    public class ProviderRepository : IProviderRepository
    {
        private readonly IConfiguration _config;

        public ProviderRepository(IConfiguration config)
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

        public async Task<Provider> GetByID(int id)
        {
            using (var con = mainConn)
            {
                string query = "SELECT * FROM dbo.Providers WHERE ID = @ID";
                var result = await con.QueryFirstOrDefaultAsync<Provider>(query, new { ID = id });
                return result;
            }
        }

    }
}
