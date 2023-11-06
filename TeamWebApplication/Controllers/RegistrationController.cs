using Microsoft.AspNetCore.Mvc;
using TeamWebApplication.Models;
using TeamWebApplication.Data.Database;
using TeamWebApplication.Data.ExceptionLogger;
using TeamWebApplication.Data.MailService;
using TeamWebApplication.Controllers.ControllerEventArgs;

namespace TeamWebApplication.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly ApplicationDBContext _db;
        private readonly IDataLogger _logger;
        private readonly IMailService _mailService;

        public event EventHandler<RegistrationEventArgs> Registration;

        public RegistrationController(ApplicationDBContext db, IDataLogger logger, IMailService mailService)
        {
            _db = db;
            _logger = logger;
            _mailService = mailService;
            Registration += _mailService.OnRegistration;
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
                    user.UserId = _db.Users.Single(tuser => tuser.Email == user.Email).UserId;

                    OnUserRegistration(user);
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

        protected virtual void OnUserRegistration(User user)
        {
            if (Registration != null)
            {
                _logger.Log(DateTime.Now + $": Registered {user.Name} with an id of {user.UserId}.");
                Registration.Invoke(this, new RegistrationEventArgs(user));
            }
        }
    }
}
