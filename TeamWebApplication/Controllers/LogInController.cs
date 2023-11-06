using Microsoft.AspNetCore.Mvc;
using TeamWebApplication.Data.Database;
using TeamWebApplication.Data.ExceptionLogger;
using TeamWebApplication.Data.Exceptions;
using TeamWebApplication.Data.ExtensionMethods;
using TeamWebApplication.Models;

namespace TeamWebApplication.Controllers
{
    public class LogInController : Controller
    {
		private readonly ApplicationDBContext _db;
        private readonly IDataLogger _logger;

		public LogInController(ApplicationDBContext db, IDataLogger logger)
        {
            _db = db;
            _logger = logger;
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
            try
            {
				var user = _db.Users.FirstOrDefault(user => user.UserId == login.UserId && user.Password == login.Password);
				if (user == null)
					return RedirectToAction("Index", "Login");

				HttpContext.Session.SetInt32("LoggedInUserId", user.UserId);
				HttpContext.Session.SetInt32("LoggedInUserRole", (int)user.Role);
				_db.SaveChanges();
				return RedirectToAction("Index", "Course");
			}
            catch (SessionCredentialException ex)
            {
                _logger.Log(ex);
				return RedirectToAction("Index", "Home");
			}
            catch (Exception ex1)
            {
                _logger.Log(ex1);
                throw;
            }
        }
    }
}