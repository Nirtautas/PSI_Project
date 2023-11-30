using Microsoft.AspNetCore.Mvc;
using TeamWebApplicationAPI.Controllers.ControllerEventArgs;
using TeamWebApplicationAPI.Data.Database;
using TeamWebApplicationAPI.Data.ExceptionLogger;
using TeamWebApplicationAPI.Data.MailService;
using TeamWebApplicationAPI.Models;
using TeamWebApplicationAPI.Repositories.Interfaces;

namespace TeamWebApplicationAPI.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IDataLogger _logger;
        private readonly IMailService _mailService;

        public event EventHandler<RegistrationEventArgs> Registration;

        public RegistrationController(IDataLogger logger, IMailService mailService, IUsersRepository usersRepository)
        {
            _logger = logger;
            _mailService = mailService;
            _usersRepository = usersRepository;
            Registration += _mailService.OnRegistration;
        }

        public IActionResult Index()
        {
            var user = new User();
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            try
            {
                if (!await _usersRepository.UserWithSuchEmailExistsAsync(user.Email))
                {
                    await _usersRepository.InsertUserAsync(user);
                    var placedUser = await _usersRepository.GetUserByEmailAsync(user.Email);
                    OnUserRegistration(placedUser);
                    return RedirectToAction("Index", "Login");
                }
                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {
                await _usersRepository.DeleteUserAsync(user);
                _logger.Log(ex);
                throw;
            }
        }

        protected virtual void OnUserRegistration(User? user)
        {
            if (Registration != null && user != null)
            {
                _logger.Log(DateTime.Now + $": Registered {user.Name} with an id of {user.UserId}.");
                Registration.Invoke(this, new RegistrationEventArgs(user));
            }
        }
    }
}
