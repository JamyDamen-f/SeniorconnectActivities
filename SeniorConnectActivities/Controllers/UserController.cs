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
    public class UserController : Controller
    {
        private readonly DbContext _dbConnection;


        public UserController(DbContext dbConnection)
        {
            _dbConnection = dbConnection;

        }


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
                return RedirectToAction("Login", "Home");
            }
        }
    }
}

