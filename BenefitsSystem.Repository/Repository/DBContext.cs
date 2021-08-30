using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace BenefitsSystem.Repository
{
    public class DBContext
    {
        private readonly string connectionString;

        public DBContext(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IDbConnection CreateConnection()
            => new SqlConnection(connectionString);
    }

}
