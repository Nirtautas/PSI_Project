using Microsoft.AspNetCore.Mvc;
using TeamWebApplication.Models;
using TeamWebApplication.Data.Database;
using TeamWebApplication.Data.ExceptionLogger;
using TeamWebApplication.Data.ExtensionMethods;

namespace TeamWebApplication.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly ApplicationDBContext _db;
        private readonly IExceptionLogger _logger;

        public RegistrationController(ApplicationDBContext db, IExceptionLogger logger)
        {
            _db = db;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var user = new User();
            return View(user);
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            try
            {
				_db.Users.Add(user);
				_db.SaveChanges();
				return RedirectToAction("Index", "Login");
			}
            catch (Exception ex)
            {
                _logger.Log(ex);
                throw;
            }
		}
    }
}
