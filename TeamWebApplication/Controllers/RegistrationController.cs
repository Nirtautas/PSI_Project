using Microsoft.AspNetCore.Mvc;
using TeamWebApplication.Models;
using TeamWebApplication.Data;
using TeamWebApplication.Data.Database;

namespace TeamWebApplication.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly ApplicationDBContext _db;
        public RegistrationController(ApplicationDBContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var user = new User();
            return View(user);
        }

        [HttpPost]//tells the routing engine to send any POST requests to that action method to the one method over the other
        public IActionResult Login(User user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();
            return RedirectToAction("Index", "Login");
        }
    }
}
