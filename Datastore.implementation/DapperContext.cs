using System;
using System.Collections.Generic;
using System.Data.Common;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;


namespace Datastore.implementation
{
    public class DapperContext
    {
        private readonly string _connectionstring;

        public DapperContext(IConfiguration confguration)
        {
            _connectionstring = confguration.GetConnectionString("DefaultConnection");

        }
        public SqlConnection createConnection()
        {
            return new SqlConnection(_connectionstring);

        }
    }
}