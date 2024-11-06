using MySql.Data.MySqlClient;

namespace SeniorConnectActivitiesCore
{
    public class DbConnection
    {
        private readonly string _connectionString;

        public DbConnection(string connectionString)
        {
            _connectionString = connectionString;
        }

        public MySqlConnection GetConnection()
        {
            var connection = new MySqlConnection(_connectionString);
            return connection;
        }
    }
}
