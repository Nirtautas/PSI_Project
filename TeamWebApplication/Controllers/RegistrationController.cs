using Microsoft.AspNetCore.Mvc;
using TeamWebApplication.Models;
using TeamWebApplication.Data.Database;
using TeamWebApplication.Data.ExceptionLogger;
using TeamWebApplication.Data.MailService;

namespace TeamWebApplication.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly ApplicationDBContext _db;
        private readonly IExceptionLogger _logger;
        private readonly IMailService _mailService;

        public RegistrationController(ApplicationDBContext db, IExceptionLogger logger, IMailService mailService)
        {
            _db = db;
            _logger = logger;
            _mailService = mailService;
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
                if (!_db.Users.Any(tuser => tuser.Email == user.Email))
                {
                    _db.Users.Add(user);
                    _db.SaveChanges();
                    int userId = _db.Users.Single(tuser => tuser.Email == user.Email).UserId;

                    _mailService.SendConfirmationEmail(user.Email, user.Name, userId);
                    return RedirectToAction("Index", "Login");
                }
                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {
                _db.Users.Remove(user);
                _db.SaveChanges();
                _logger.Log(ex);
                throw;
            }
		}
    }
}
