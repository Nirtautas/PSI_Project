using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using TeamWebApplicationAPI.Models;

namespace TeamWebApplication.Controllers
{
    public class LogInController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var http = new HttpClient();
            var response = await http.GetAsync("https://localhost:7107/api/ApiLogIn/ApiGetLoginObject");
            if (response.IsSuccessStatusCode)
            {
                var loginDetails = JsonConvert.DeserializeObject<LoginDetails>(await response.Content.ReadAsStringAsync());
                return View(loginDetails);
            }
            return RedirectToAction("Error", "Home");
        }

        public async Task<IActionResult> Registration()
        {
            var http = new HttpClient();
            var response = await http.GetAsync("https://localhost:7107/api/ApiLogIn/ApiRegistration");
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index", "Registration");
            return RedirectToAction("Error", "Home");
        }

        public async Task<IActionResult> Login(LoginDetails login)
        {
            var http = new HttpClient();
            var content = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");
            var response = await http.PostAsync("https://localhost:7107/api/ApiLogIn/ApiLogin", content);
            if (response.IsSuccessStatusCode)
            {
                var user = JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync());
                HttpContext.Session.SetInt32("LoggedInUserId", user.UserId);
                HttpContext.Session.SetInt32("LoggedInUserRole", (int)user.Role);
                return RedirectToAction("Index", "Course");
            }
            else
                return RedirectToAction("Index","LogIn");
        }
    }
}