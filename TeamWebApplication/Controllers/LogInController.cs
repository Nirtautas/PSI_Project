using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TeamWebApplication.Data;
using TeamWebApplication.Models;

namespace TeamWebApplication.Controllers
{
    public class LogInController : Controller
    {
        private readonly UserContainer _userContainer;//readonly - constant

        public LogInController(IUserContainer userContainer)
        {
            _userContainer = userContainer;//dependency injection
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
        public IActionResult Login(LoginDetails login)//login details from frontend
        {
            var user = _userContainer._userList.Where(user => user.Password == login.Password && user.UserId == login.UserId);
            if (!user.Any())
            {
                return RedirectToAction("Index", "Login", null); ;//user not found}
            }
            return RedirectToAction("Index", "Course", user);//Index - action, Course - controller, user - object (user that signed in)
        }
    }
}