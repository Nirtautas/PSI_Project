using Microsoft.AspNetCore.Mvc;
using MyWebApplication.Models;

namespace MyWebApplication.Controllers
{
    public class CourseController : Controller
    {
        public IActionResult Course()
        {
            return View();
        }
    }
}
