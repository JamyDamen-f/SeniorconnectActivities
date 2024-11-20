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
        private AuthenticationService authenticationService = new AuthenticationService();

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

        public IActionResult CheckLogin(UserModel user)
        {
            if (authenticationService.Login(user.Email, user.Password))
            {
                return View("Index");
            }
            else
            {
                ViewBag.ErrorMessage = "De combinatie van je email en wachtwoord komen niet overeen.";
                return View("Login");
            }
            //TODO: Roep methode aan van class library die checkt of de login goed is
            // Ja - maak sessie aan en redirect hem naar de welkom pagina
            // Nee - geef een error message etzelfde als bij add en edit van activiteit



        }
    }
}
