using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using SeniorConnectActivities.Models;
using SeniorConnectActivities.Models.Entities;
using SeniorConnectActivitiesCore;
using System.Data;
namespace SeniorConnectActivities.Controllers
{
    public class ActivitiesController : Controller
    {
        private readonly DbContext _dbConnection;

        public ActivitiesController(DbContext dbConnection)
        {
            _dbConnection = dbConnection;
        }


        /// <summary>
        /// Get all Activities
        /// </summary>
        /// <returns>A view with a list of activities</returns>
        [HttpGet]
        public async Task<IActionResult> Index()
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
                var command = new MySqlCommand("SELECT * FROM activity", connection);
                var reader = await command.ExecuteReaderAsync();

                // Create a list of activities
                List<ActivityModel> Activities = new List<ActivityModel>();

                // Fill the list with activities
                while (await reader.ReadAsync())
                {
                    var activity = new ActivityModel();
                    {
                        activity.Id = reader.GetInt32(0);
                        activity.Title = reader.GetString(1);
                        activity.Description = reader.IsDBNull(2) ? null : reader.GetString(2);
                        activity.Location = reader.GetString(3);
                        activity.Start = reader.GetDateTime(4);
                        activity.End = reader.GetDateTime(5);
                        activity.MaxParticipants = reader.GetInt32(6);
                        activity.Created = reader.GetDateTime(7);
                        activity.LastUpdated = reader.GetDateTime(8);
                        activity.Url = reader.IsDBNull(9) ? null : reader.GetString(9);
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


        /// <summary>
        /// Adds a activityEntity
        /// </summary>
        /// <param name="model"></param>
        /// <returns>A view with the added Activity</returns>
        [HttpPost]
        public async Task<IActionResult> AddEntity(ActivityModel model)
        {
            if (ModelState.IsValid)
            {
                try
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
                        Title = model.Title,
                        Description = model.Description,
                        Location = model.Location,
                        Start = model.Start,
                        End = model.End,
                        MaxParticipants = model.MaxParticipants,
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
                    // Return view
                    return RedirectToAction("Index", "Activities");
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else 
            {
                return View("CreateEditEntity", model);
            }
        }


        /// <summary>
        /// Get activiteit
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Activiteit if id is given</returns>
        [HttpGet]
        public async Task<IActionResult> CreateEditEntity(int? id)
        {
            if (id != null)
            {
                // Open connection
                var connection = _dbConnection.GetConnection();
                if (connection.State != ConnectionState.Open)
                {
                    await connection.OpenAsync();
                }

                // Sql command
                var command = new MySqlCommand("SELECT id, title, description, location, start, end, max_participants, created_at, updated_at, image_url FROM activity WHERE id = @id", connection);

                // Command parameters
                command.Parameters.AddWithValue("@id", id);

                // Execute command
                var reader = await command.ExecuteReaderAsync();
                reader.ReadAsync();

                // Fill the activity with the id from the list
                var activity = new ActivityModel();
                activity.Id = reader.GetInt32(0);
                activity.Title = reader.GetString(1);
                activity.Description = reader.IsDBNull(2) ? null : reader.GetString(2);
                activity.Location = reader.GetString(3);
                activity.Start = reader.GetDateTime(4);
                activity.End = reader.GetDateTime(5);
                activity.MaxParticipants = reader.GetInt32(6);
                activity.Created = reader.GetDateTime(7);
                activity.LastUpdated = reader.GetDateTime(8);
                activity.Url = reader.IsDBNull(9) ? null : reader.GetString(9);

                // Close reading and the db connection
                await reader.CloseAsync();
                await connection.CloseAsync();

                // Return view
                return View(activity);
            }
            return View();
        }


        /// <summary>
        /// Edit the values of the activity that is given
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Index view</returns>
        [HttpPost]
        public async Task<IActionResult> EditEntity(ActivityModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Open connection
                    var connection = _dbConnection.GetConnection();
                    if (connection.State != ConnectionState.Open)
                    {
                        await connection.OpenAsync();
                    }

                    // Sql command
                    var command = new MySqlCommand("UPDATE activity SET title = @title, description = @description, location = @location, start = @start, end = @end, max_participants = @max_participants, created_at = @created_at, updated_at = @updated_at, image_url = @image_url WHERE id = @id;", connection);

                    // Command parameters
                    command.Parameters.AddWithValue("@id", model.Id);
                    command.Parameters.AddWithValue("@title", model.Title);
                    command.Parameters.AddWithValue("@description", model.Description ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@location", model.Location);
                    command.Parameters.AddWithValue("@start", model.Start);
                    command.Parameters.AddWithValue("@end", model.End);
                    command.Parameters.AddWithValue("@max_participants", model.MaxParticipants);
                    command.Parameters.AddWithValue("@created_at", model.Created);
                    command.Parameters.AddWithValue("@updated_at", model.LastUpdated);
                    command.Parameters.AddWithValue("@image_url", model.Url ?? (object)DBNull.Value);

                    // Execute the command
                    await command.ExecuteNonQueryAsync();

                    // Close connection
                    await connection.CloseAsync();

                    // Return view
                    return RedirectToAction("Index", "Activities");
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                return View("CreateEditEntity", model);
            }
        }


        /// <summary>
        /// Get the entity that is selected in the view
        /// </summary>
        /// <returns>returns the details page with the selected entity</returns>
        [HttpGet]
        public async Task<IActionResult> DetailsEntity(int id)
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
                var command = new MySqlCommand("SELECT title, description, location, start, end, max_participants, created_at, updated_at, image_url FROM activity WHERE id = @id", connection);

                // Command parameters
                command.Parameters.AddWithValue("@id", id);
                var reader = await command.ExecuteReaderAsync();
                reader.ReadAsync();

                // Fill the activity with the activity with a specific id
                var activity = new ActivityModel();
                activity.Title = reader.GetString(0);
                activity.Description = reader.IsDBNull(1) ? null : reader.GetString(1);
                activity.Location = reader.GetString(2);
                activity.Start = reader.GetDateTime(3);
                activity.End = reader.GetDateTime(4);
                activity.MaxParticipants = reader.GetInt32(5);
                activity.Created = reader.GetDateTime(6);
                activity.LastUpdated = reader.GetDateTime(7);
                activity.Url = reader.IsDBNull(8) ? null : reader.GetString(8);


                // Close reading and the db connection
                await reader.CloseAsync();
                await connection.CloseAsync();

                // Return view 
                return View("Details", activity);
            }
            catch (Exception)
            {
                throw;
            }

        }


        /// <summary>
        /// Deletes a entity from activity
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Index View </returns>
        public async Task<IActionResult> DeleteEntity(int id)
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
                var command = new MySqlCommand("DELETE FROM activity WHERE id = @id", connection);
                // Command parameters
                command.Parameters.AddWithValue("@id", id);

                // Execute the command
                await command.ExecuteNonQueryAsync();

                // Close connection
                await connection.CloseAsync();

                // Return view
                return RedirectToAction("Index", "Activities");
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}

