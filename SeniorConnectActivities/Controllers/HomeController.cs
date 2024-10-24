using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using SeniorConnectActivities.Models;
using SeniorConnectActivities.Models.Entities;
using System.Diagnostics;

namespace SeniorConnectActivities.Controllers
{
    public class HomeController(MySqlConnection mySqlConnection) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /// <summary>
        /// Makes a dbconnection and get all activities
        /// </summary>
        /// <returns>A view with a list of activities</returns>
        /*public async Task<IActionResult> Activities()
        {
            try
            {
                List<ActivityModel> Activities = new List<ActivityModel>();

                // Fetch data from the database using ApplicationDbContext's GetConnection method

                await mySqlConnection.OpenAsync();

                var command = new MySqlCommand("SELECT title, description, location, start, end, max_participants FROM seniorconnectdb.activity", mySqlConnection);
                var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    var activity = new ActivityModel();
                    {
                        activity.Title = reader.GetString(0);
                        activity.Description = reader.GetString(1);
                        activity.Location = reader.GetString(2);
                        activity.Start = reader.GetDateTime(3);
                        activity.End = reader.GetDateTime(4);
                        activity.MaxParticipants = reader.GetInt32(5);
                    };
                    Activities.Add(activity);
                }
                await mySqlConnection.CloseAsync();

                //Pass the list of activities to the view
                return View(Activities);
            }
            catch (Exception)
            {
                throw;
            }
        }*/
    }
}
