using Microsoft.AspNetCore.Mvc;
using TeamWebApplication.Data.Database;
using TeamWebApplication.Data.ExceptionLogger;
using TeamWebApplication.Data.Exceptions;
using TeamWebApplication.Models;
using TeamWebApplication.Repositories.Interfaces;

namespace TeamWebApplication.Controllers
{
    public class LogInController : Controller
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IDataLogger _logger;

		public LogInController(IDataLogger logger, IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
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
        public async Task<IActionResult> Login(LoginDetails login)
        {
            try
            {
                var user = await _usersRepository.GetUserByCredentials(login.UserId, login.Password);
				if (user == null)
					return RedirectToAction("Index", "Login");

				HttpContext.Session.SetInt32("LoggedInUserId", user.UserId);
				HttpContext.Session.SetInt32("LoggedInUserRole", (int)user.Role);
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