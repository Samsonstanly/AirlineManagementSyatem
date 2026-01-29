using AirlineManagementSyatem.Models;
using ClassLibraryDatabaseConnection;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagementSyatem.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly string _connectionString;

        public AdminRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["Airline"].ConnectionString;
        }

        public async Task<Admin> LoginAsync(string username, string password)
        {
            await using SqlConnection conn = ConnectionManager.OpenConnection(_connectionString);
            await using SqlCommand cmd = new SqlCommand(
                "SELECT * FROM Admin WHERE UserName=@UserName AND Password=@Password", conn);

            cmd.Parameters.AddWithValue("@UserName", username);
            cmd.Parameters.AddWithValue("@Password", password);

            await using var reader = await cmd.ExecuteReaderAsync();

            return await reader.ReadAsync()
                ? new Admin
                {
                    AdminId = (int)reader["AdminId"],
                    UserName = reader["UserName"].ToString()
                }
                : null;
        }
    }
}
