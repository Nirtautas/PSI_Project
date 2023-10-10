using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TeamWebApplication.Data;
using TeamWebApplication.Data.Database;
using TeamWebApplication.Models;

namespace TeamWebApplication.Controllers
{
    public class LogInController : Controller
    {
        private readonly IUserContainer _userContainer;
		private readonly ApplicationDBContext _db;

		public LogInController(IUserContainer userContainer, ApplicationDBContext db)
        {
            _db = db;
            _userContainer = userContainer;
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
            var dbUser = _db.Users.FirstOrDefault(dbUser => dbUser.UserId == login.UserId && dbUser.Password == login.Password);
			if (dbUser == null)
				return RedirectToAction("Index", "Login");
			_userContainer.loggedInUserId = dbUser.UserId;
			_userContainer.loggedInUserRole = dbUser.Role;
			return RedirectToAction("Index", "Course");
        }
    }
}