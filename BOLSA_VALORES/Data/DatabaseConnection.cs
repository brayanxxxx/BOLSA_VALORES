using System;
using System.Data.SqlClient;

namespace BOLSA_VALORES.Data
{
    public class DatabaseConnection
    {
        private static DatabaseConnection _instance;
        private static readonly object _lock = new object();
        private readonly string _connectionString;
        private SqlConnection _connection;

     
        private DatabaseConnection()
        {
            _connectionString = @"Server=localhost;Database=BOLSA_VALORES;Trusted_Connection=True;";
        }


        public static DatabaseConnection Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new DatabaseConnection();
                    }
                    return _instance;
                }
            }
        }


        public SqlConnection GetConnection()
        {
            if (_connection == null)
                _connection = new SqlConnection(_connectionString);

            if (_connection.State == System.Data.ConnectionState.Closed)
                _connection.Open();

            return _connection;
        }


        public void CloseConnection()
        {
            if (_connection != null && _connection.State != System.Data.ConnectionState.Closed)
            {
                _connection.Close();
            }
        }
    }
}
