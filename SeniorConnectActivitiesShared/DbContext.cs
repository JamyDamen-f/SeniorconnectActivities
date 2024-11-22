using MySql.Data.MySqlClient;

namespace SeniorConnectActivitiesCore
{
    public class DbContext
    {
        private readonly string _connectionString;

        public DbContext(string connectionString)
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
