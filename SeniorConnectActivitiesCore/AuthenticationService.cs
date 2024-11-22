using MySql.Data.MySqlClient;
using SeniorConnectActivitiesShared.Models;
using System.Data;

namespace SeniorConnectActivitiesCore
{
    public class AuthenticationService
    {
        private readonly DbContext _dbConnection;

        public AuthenticationService(DbContext dbConnection)
        {
            _dbConnection = dbConnection;
        }


        /// <summary>
        /// Get all users and check if the given login match with a user in the list
        /// </summary>
        /// <param name="formEmail"></param>
        /// <param name="formPassword"></param>
        /// <returns>If login is true or false</returns>
        public async Task<bool> Login(string formEmail, string formPassword)
        {
            // Opens db connection
            var connection = _dbConnection.GetConnection();
            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync();
            }

            // Sql command and execute the command
            var command = new MySqlCommand("SELECT u.id, u.role_id, u.email, u.password, u.active, u.google_id, u.facebook_id, u.first_name, u.middle_name, u.last_name, u.name_affix, u.address, u.postal_code, u.country, u.phone_number, u.date_of_birth, u.profile_picture_url, u.created_at, u.updated_at, r.name AS role_name FROM user u LEFT JOIN user_role r ON u.role_id = r.id;", connection);

            List<UserModel> users = new List<UserModel>();

            var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                // Fill the user from with the unique email and password
                var user = new UserModel();
                {
                    user.Id = reader.GetInt32(0);
                    user.RolId = new RolModel
                    {
                        Id = reader.GetInt32(1),
                        Name = reader.GetString(19)
                    };
                    user.Email = reader.GetString(2);
                    user.Password = reader.GetString(3);
                    user.Active = reader.GetBoolean(4);
                    user.GoogleId = reader.IsDBNull(5) ? null : reader.GetString(5);
                    user.FacebookId = reader.IsDBNull(6) ? null : reader.GetString(6);
                    user.FirstName = reader.GetString(7);
                    user.MiddleName = reader.IsDBNull(8) ? null : reader.GetString(8);
                    user.LastName = reader.GetString(9);
                    user.NameAffix = reader.IsDBNull(10) ? null : reader.GetString(10);
                    user.Address = reader.GetString(11);
                    user.PostalCode = reader.GetString(12);
                    user.Country = reader.GetString(13);
                    user.Phonenumber = reader.GetString(14);
                    user.DateOfBirth = reader.GetDateTime(15);
                    user.picture = reader.IsDBNull(16) ? null : reader.GetString(16);
                    user.Created = reader.GetDateTime(17);
                    user.LastUpdated = reader.GetDateTime(18);
                }
                users.Add(user);
            }

            // Close reading and the db connection
            await reader.CloseAsync();
            await connection.CloseAsync();
            
            var matchingUser = users.FirstOrDefault(u => u.Email == formEmail && u.Password == formPassword);
            return matchingUser != null;
        }
    }
}

