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
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace GlobalMentalityAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _config;

        public UserRepository(IConfiguration config)
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

        public async Task<int> InsertUser(User user)
        {
            using (var con = mainConn)
            {
                string query = @"INSERT INTO [dbo].[Users]
                                   (Username, Password, Email, DateCreated, Role)
                             VALUES
                                (@Username, @Password, @Email, @DateCreated, @Role)
                             SELECT SCOPE_IDENTITY()";
                                ;
                user.DateCreated = DateTime.Now;
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                var id = await con.QueryFirstOrDefaultAsync<int>(query, user);
                return id;
            }
        }

        public async Task<LoggedUser> Login(LoginUser user)
        {
            using (var con = mainConn)
            {
                string query = @"SELECT * FROM dbo.Users WHERE Username = @Username";
                var sqldata = await con.QueryFirstOrDefaultAsync<User>(query, user);

                //if no matching user
                if (sqldata == null)
                    return null;

                //if password incorrect
                if (!BCrypt.Net.BCrypt.Verify(user.Password, sqldata.Password))
                {
                    return null;
                }

                var userData = new LoggedUser()
                {
                    ID = sqldata.ID,
                    Role = sqldata.Role
                };

                // authentication successful so generate jwt token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_config.GetValue<string>("JWTSecret"));
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, userData.ID.ToString()),
                    new Claim(ClaimTypes.Role, userData.Role)
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                userData.Token = tokenHandler.WriteToken(token);

                return userData;
            }
        }
    }
}
