using MySql.Data.MySqlClient;

namespace SeniorConnectActivities.Data
{
    public class ApplicationDbContext
    {
        public string ConnectionString { get; set; }

        // Constructor accepting the connection string via dependency injection
        public ApplicationDbContext(string connectionString)
        {
            ConnectionString = connectionString;
        }

        // Helper method to open a MySQL connection
        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }
    }
}
