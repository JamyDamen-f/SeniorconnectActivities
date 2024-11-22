using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using SeniorConnectActivitiesCore;
using SeniorConnectActivitiesShared.Models;
using System;
using System.Data;
using System.Diagnostics.Metrics;

namespace SeniorConnectActivities.Controllers
{
    public class LoginController : Controller
    {
        private readonly DbContext _dbConnection;


        public LoginController(DbContext dbConnection)
        {
            _dbConnection = dbConnection;

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Registration()
        {
            return View();
        }


        /// <summary>
        /// Shows page based on login is true or false 
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns>View</returns>
        public async Task<IActionResult> CheckLogin(UserModel userLogin)
        {
            var authenticationService = new AuthenticationService(_dbConnection);

            if (await authenticationService.Login(userLogin.Email, userLogin.Password))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["ErrorMessage"] = "De combinatie van je email en wachtwoord komen niet overeen.";
                return RedirectToAction("Index", "Login");
            }
        }
    }
}

