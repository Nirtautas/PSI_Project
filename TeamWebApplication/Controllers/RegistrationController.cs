using Microsoft.AspNetCore.Mvc;
using TeamWebApplication.Models;
using System.Diagnostics;

namespace TeamWebApplication.Controllers
{
    public class RegistrationController : Controller
    {
        public IActionResult Index()
        {
            var user = new Student();
            return View(user);
        }

        [HttpPost]//tells the routing engine to send any POST requests to that action method to the one method over the other
        public IActionResult Login(Student user)
        {
            //User.ToString();
            return RedirectToAction("Index", "Login");
        }

    }
}
