using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BenefitsSystem.Web.Repository
{
    public class DBContext 
    {
        //private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public DBContext(IConfiguration configuration)
        {
           // _configuration = configuration;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public DBContext(string connectionString)
        {
            // _configuration = configuration;
            _connectionString = connectionString;
        }
        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);
    }
}
