using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Mysqlx.Datatypes;
using SeniorConnectActivities.Data;
using SeniorConnectActivities.Models.Entities;
using System.Data.Common;
using System.Text;


namespace SeniorConnectActivities.Controllers
{

    public class ActivityController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ActivityController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Activities()
        {
            try
            {
                List<UserModel> Activities = new List<UserModel>();

                // Fetch data from the database using ApplicationDbContext's GetConnection method
                using (var connection = _context.GetConnection())
                {
                    await connection.OpenAsync();

                    var command = new MySqlCommand("SELECT first_name, last_name FROM seniorconnectdb.user;", connection);
                    var reader = await command.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        var user = new UserModel
                        {
                            FirstName = reader.GetString(0),
                            LastName = reader.GetString(1)
                        };
                        Activities.Add(user);
                    }

                    await connection.CloseAsync();
                }

                if (Activities == null || Activities.Count == 0)
                {
                    Activities = new List<UserModel>(); // Initialize an empty list if no activities found
                }
                //Pass the list of activities to the view
                return View(Activities);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
