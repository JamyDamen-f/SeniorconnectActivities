using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using SeniorConnectActivities.Models;
using SeniorConnectActivities.Models.Entities;
using SeniorConnectActivitiesCore;
using System.Diagnostics;

namespace SeniorConnectActivities.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Registration()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
