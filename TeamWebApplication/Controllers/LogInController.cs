using Microsoft.AspNetCore.Mvc;
using TeamWebApplication.Data.Database;
using TeamWebApplication.Models;

namespace TeamWebApplication.Controllers
{
    public class LogInController : Controller
    {
		private readonly ApplicationDBContext _db;

		public LogInController(ApplicationDBContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var loginDetails = new LoginDetails();
            return View(loginDetails);
        }

        public IActionResult Registration()
        {
            return RedirectToAction("Index", "Registration");
        }

        [HttpPost]
        public IActionResult Login(LoginDetails login)
        {
            var user = _db.Users.FirstOrDefault(user => user.UserId == login.UserId && user.Password == login.Password);
			if (user == null)
				return RedirectToAction("Index", "Login");
			
            HttpContext.Session.SetInt32("LoggedInUserId", user.UserId);
            HttpContext.Session.SetInt32("LoggedInUserRole", (int)user.Role);
			_db.SaveChanges();
			return RedirectToAction("Index", "Course");
        }
    }
}