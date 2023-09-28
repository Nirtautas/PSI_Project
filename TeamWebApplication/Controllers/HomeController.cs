using Microsoft.AspNetCore.Mvc;
using TeamWebApplication.Models;
using System.Diagnostics;
using TeamWebApplication.Data;

namespace TeamWebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserContainer _userContainer;

        public HomeController(ILogger<HomeController> logger, IUserContainer userContainer)
        {
            _logger = logger;
            _userContainer = userContainer;
        }

        public IActionResult Index()
        {
            _userContainer.loggedInUserId = 0;
            _userContainer.loggedInUserRole = null;
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