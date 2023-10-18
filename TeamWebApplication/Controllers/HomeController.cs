using Microsoft.AspNetCore.Mvc;
using TeamWebApplication.Models;
using TeamWebApplication.Models.Enums;
using System.Diagnostics;
using TeamWebApplication.Data.Database;

namespace TeamWebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDBContext _db;

        public HomeController(ApplicationDBContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            HttpContext.Session.Remove("LoggedInUserId");
            HttpContext.Session.Remove("LoggedInUserRole");
            HttpContext.Session.Remove("CurrentCourseId");
			_db.SaveChanges();
			return View();
        }

        public IActionResult Registration()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}