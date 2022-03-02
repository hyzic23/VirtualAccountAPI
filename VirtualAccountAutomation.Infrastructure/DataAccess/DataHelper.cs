using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace VirtualAccountAutomation.Infrastructure.DataAccess
{
    public class DataHelper
    {
        private readonly IConfiguration configuration;

        public DataHelper(IConfiguration configuration)
        {
            this.configuration = configuration;
        }


        protected IDbConnection CreateConnection()
        {
            return new SqlConnection(configuration.GetConnectionString("VirtualAccountConnection"));
        }
    }
}