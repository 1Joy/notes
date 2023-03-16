using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperDemo
{
    public  class DbContext
    {
        public static string ConnectionString
        {
            get => "connection";
        }

        private static IDbConnection _dbConnection;
        public static IDbConnection DbConnection
        {
            get
            {
                if( _dbConnection == null)
                {
                    _dbConnection = new MySqlConnection();
                    _dbConnection.ConnectionString = ConnectionString;
                }
                return _dbConnection;
            }
        }
    }
}
