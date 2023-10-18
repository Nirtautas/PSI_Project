using Microsoft.AspNetCore.Mvc;
using TeamWebApplication.Models;
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

        [HttpPost]
        public IActionResult Login(User user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();
            return RedirectToAction("Index", "Login");
        }
    }
}
