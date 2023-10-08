using Microsoft.AspNetCore.Mvc;
using TeamWebApplication.Data;
using TeamWebApplication.Models;

namespace TeamWebApplication.Controllers
{
    public class LogInController : Controller
    {
        private readonly IUserContainer _userContainer;

        public LogInController(IUserContainer userContainer)
        {
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
        public IActionResult Login(LoginDetails login)//login details from frontend
        {
            var user = _userContainer.userList.SingleOrDefault(user => user.Password == login.Password && user.UserId == login.UserId);
			if (user == null)
                return RedirectToAction("Index", "Login");//user was not found}
            _userContainer.loggedInUserId = user.UserId;
            _userContainer.loggedInUserRole = user.Role;
             return RedirectToAction("Index", "Course");
        }
    }
}