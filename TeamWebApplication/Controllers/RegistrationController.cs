using Microsoft.AspNetCore.Mvc;
using TeamWebApplication.Models;
using System.Diagnostics;

namespace TeamWebApplication.Controllers
{
    public class RegistrationController : Controller
    {
        public IActionResult Index()
        {
            var user = new User();
            return View(user);
        }

        [HttpPost]
        public IActionResult Test(User user)
        {
            Debug.WriteLine(user.Name);
            Debug.WriteLine(user.Surname);
            Debug.WriteLine(user.Email);
            Debug.WriteLine(user.Password);
            Debug.WriteLine(user.Faculty);
            Debug.WriteLine(user.Specialization);
            Debug.WriteLine(user.Role);
            return View();
        }

    }
}
