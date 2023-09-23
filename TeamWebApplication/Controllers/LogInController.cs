using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TeamWebApplication.Data;
using TeamWebApplication.Models;

namespace TeamWebApplication.Controllers
{
    public class LogInController : Controller
    {
        private readonly IUserContainer _userContainer;//readonly - constant

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
                return RedirectToAction("Index", "Login"); ;//user not found}
            _userContainer.loggedInUserId = user.UserId;
			return RedirectToAction("Index", "Course");//Index - action, Course - controller, user - object (user that signed in)
        }
    }
}