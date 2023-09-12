using Microsoft.AspNetCore.Mvc;
using TeamWebApplication.Models;
using System.Diagnostics;

namespace TeamWebApplication.Controllers
{
    public class RegistrationController : Controller
    {
        public IActionResult Index()
        {
            var user = new Student();
            return View(user);
        }

        [HttpPost]
        public IActionResult Login(Student user)
        {
            //User.ToString();
            //Debug.WriteLine(user.Name);
            //Debug.WriteLine(user.Surname);
            //Debug.WriteLine(user.Email);
            //Debug.WriteLine(user.Password);
            //Debug.WriteLine(user.Faculty);
            //Debug.WriteLine(user.Specialization);
            //Debug.WriteLine(user.Role);
            return RedirectToAction("Index", "Login");
        }

    }
}
