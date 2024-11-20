using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using SeniorConnectActivities.Models.Entities;
using SeniorConnectActivitiesCore;
using System.Data;

namespace SeniorConnectActivities.Controllers
{
    public class UserController : Controller
    {
        private readonly DbContext _dbConnection;
        private AuthenticationService authenticationService = new AuthenticationService();


        public UserController(DbContext dbConnection)
        {
            _dbConnection = dbConnection;
        }


        public async Task<IActionResult> CheckLogin(UserModel userLogin)
         {
            try
            {
                // Opens db connection
                var connection = _dbConnection.GetConnection();
                if (connection.State != ConnectionState.Open)
                {
                    await connection.OpenAsync();
                }

                // Sql command and execute the command
                var command = new MySqlCommand("SELECT email, password FROM user WHERE email = @email and password = @password", connection);

                // Command parameters
                command.Parameters.AddWithValue("email", userLogin.Email);
                command.Parameters.AddWithValue("password", userLogin.Password);

                var reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    // Fill the user from with the unique email and password
                    var user = new UserModel();
                    {
                        user.Email = reader.GetString(1);
                        user.Password = reader.GetString(2);
                    }
                    // Close reading and the db connection
                    await reader.CloseAsync();
                    await connection.CloseAsync();

                    if (await authenticationService.Login(userLogin.Email, user.Email, userLogin.Password, user.Password))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "De combinatie van je email en wachtwoord komen niet overeen.";
                        return RedirectToAction("Login", "Home");
                    }
                }
                else
                {
                    // No user found
                    await reader.CloseAsync();
                    await connection.CloseAsync();

                    TempData["ErrorMessage"] = "No matching user found.";
                    return RedirectToAction("Login", "Home");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
