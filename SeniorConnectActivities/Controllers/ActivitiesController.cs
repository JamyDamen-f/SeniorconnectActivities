using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using SeniorConnectActivities.Models.Entities;
using SeniorConnectActivitiesCore;
using System.Data;
namespace SeniorConnectActivities.Controllers
{
    public class ActivitiesController : Controller
    {
        private readonly DbConnection _dbConnection;

        public ActivitiesController(DbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        /// <summary>
        /// Get all Activities
        /// </summary>
        /// <returns>A view with a list of activities</returns>
        public async Task<IActionResult> Index()
        {
            try
            {
                // Create a list of activities
                List<ActivityModel> Activities = new List<ActivityModel>();
                // Opens db connection
                var connection = _dbConnection.GetConnection();
                if (connection.State != ConnectionState.Open)
                {
                    await connection.OpenAsync();
                }
                // Sql command and execute the command
                var command = new MySqlCommand("SELECT title, description, location, start, end, max_participants FROM activity", connection);
                var reader = await command.ExecuteReaderAsync();
                // Fill the list with activities
                while (await reader.ReadAsync())
                {
                    var activity = new ActivityModel();
                    {
                        activity.Title = reader.GetString(0);
                        activity.Description = reader.IsDBNull(1) ? null : reader.GetString(1);
                        activity.Location = reader.GetString(2);
                        activity.Start = reader.GetDateTime(3);
                        activity.End = reader.GetDateTime(4);
                        activity.MaxParticipants = reader.GetInt32(5);
                    };
                    Activities.Add(activity);
                }
                // Close reading and the db connection
                await reader.CloseAsync();
                await connection.CloseAsync();

                //Pass the list of activities to the view
                return View(Activities);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(ActivityModel activity)
        {
            // Open connection
            var connection = _dbConnection.GetConnection();
            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync();
            }
            // Sql command
            var command = new MySqlCommand("INSERT INTO activity (title, description, location, start, end, max_participants, created_at, updated_at, image_url)\r\n" +
                                           "VALUES (@title, @description, @location, @start, @end, @max_participants, @created_at, @updated_at, @image_url)", connection);
            // Make a new activitie
            var AddActivitie = new ActivityModel
            {
                Title = activity.Title,
                Description = activity.Description,
                Location = activity.Location,
                Start = activity.Start,
                End = activity.End,
                MaxParticipants = activity.MaxParticipants,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
            };

            // Setting up command parameters
            command.Parameters.AddWithValue("@title", AddActivitie.Title);
            command.Parameters.AddWithValue("@description", AddActivitie.Description ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@location", AddActivitie.Location);
            command.Parameters.AddWithValue("@start", AddActivitie.Start);
            command.Parameters.AddWithValue("@end", AddActivitie.End);
            command.Parameters.AddWithValue("@max_participants", AddActivitie.MaxParticipants);
            command.Parameters.AddWithValue("@created_at", AddActivitie.Created);
            command.Parameters.AddWithValue("@updated_at", AddActivitie.LastUpdated);
            command.Parameters.AddWithValue("@image_url", AddActivitie.Url ?? (object)DBNull.Value);

            // Execute the command
            await command.ExecuteNonQueryAsync();

            // Close connection
            await connection.CloseAsync();

            if (!ModelState.IsValid)
            {
                return View(activity);
            }
            // Return view
            return RedirectToAction("Index", "Activities");
        }

    }
}

