using Microsoft.AspNetCore.Mvc;
using TeamWebApplication.Models;
using TeamWebApplication.Data;
using System.Diagnostics;

namespace TeamWebApplication.Controllers
{
    public class RegistrationController : Controller
    {
         private readonly IUserContainer _userContainer;
        public RegistrationController(IUserContainer userContainer)
        {
            _userContainer = userContainer;
        }
        public IActionResult Index()
        {
            var user = new User();
            return View(user);
        }

        [HttpPost]//tells the routing engine to send any POST requests to that action method to the one method over the other
        public IActionResult Login(User user)
        {
            _userContainer.CreateUser(user);
            _userContainer.WriteUsers();
            return RedirectToAction("Index", "Login");
        }
    }
}
