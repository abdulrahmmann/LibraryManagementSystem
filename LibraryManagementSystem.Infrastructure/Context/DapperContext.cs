using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace LibraryManagementSystem.Infrastructure.Context
{
    public class DapperContext
    {
        #region Instance Fields
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        #endregion

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        }

        // Method to return a database connection
        public IDbConnection GetConnection() => new SqlConnection(_connectionString);
    }
}
