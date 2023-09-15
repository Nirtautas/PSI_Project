using Microsoft.AspNetCore.Mvc;
using TeamWebApplication.Models;

namespace TeamWebApplication.Controllers
{
    public class CourseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
