using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TeamWebApplication.Models;

namespace TeamWebApplication.Controllers
{
    public class LogInController : Controller
    {
        public IActionResult Index()
        {

            var loginDetails = new LoginDetails();
            return View(loginDetails);
        }

        public IActionResult Registration()
        {

            return RedirectToAction("Index", "Registration");

        }
        public IActionResult Login(LoginDetails login)
        {
            return RedirectToAction("Index", "Course");
        }
    }

    //namespace TeamWebApplication.Controllers
    //{
    //    public class LogInController : Controller
    //    {
    //        public LogInController()
    //        {
    //            private readonly LocalDataContainer _localDataContainer;

    //        public MyController(LocalDataContainer localDataContainer)
    //        {
    //            _localDataContainer = localDataContainer;

    //        }
    //    }
    //    public IActionResult Index()
    //    {

    //        var loginDetails = new LoginDetails();
    //        return View(loginDetails);
    //    }
    //    public IActionResult Login(LoginDetails login)
    //    {

    //        foreach (var user in _localDataContainer.UserList)
    //        {
    //            if (user.UserId == login.UserId)
    //            {
    //                if (user.Password == login.Password)
    //                {// after user successfully logs in, they'll be redirected to Course page
    //                    return RedirectResult("/Course");// succesfull log in//linq
    //                }
    //                return UnauthorizedResult();// wrong password
    //            }
    //        }
    //        return ViewNotFoundEventData();//user not found
    //    }
    //}
}
