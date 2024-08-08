using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ClinicaVeterinariaWebApp.Models;

namespace ClinicaVeterinariaWebApp.Services
{
    public class AuthService : IAuthService
    {
        private readonly string _connectionString;

        public AuthService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ClinicaVeterinaria");
        }

        public ApplicationUser Login(string username, string password)
        {
            ApplicationUser user = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT Id, Username, Password, Role FROM Users WHERE Username = @username AND Password = @password";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new ApplicationUser
                            {
                                Id = reader.GetInt32(0),
                                Username = reader.GetString(1),
                                Password = reader.GetString(2),
                                Role = reader.GetString(3)
                            };
                        }
                    }
                }
            }
            return user;
        }
    }
}
