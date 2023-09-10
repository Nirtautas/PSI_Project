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
        public IActionResult Test(LoginDetails login)
        {

            Debug.WriteLine(login.UserId);
            Debug.WriteLine(login.Password);
            return View();
        }
    }
}
