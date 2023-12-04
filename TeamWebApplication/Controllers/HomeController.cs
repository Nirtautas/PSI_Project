using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using TeamWebApplicationAPI.Models;

namespace TeamWebApplication.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            HttpContext.Session.Clear();
            var http = new HttpClient();
            var response = await http.GetAsync("https://localhost:7107/api/ApiHome/ApiIndex");
            Debug.WriteLine(response.StatusCode);
            if (response.StatusCode == HttpStatusCode.OK)
                return View();
            else
                return RedirectToAction("Error", "Home");
        }

        public async Task<IActionResult> Registration()
        {
            var http = new HttpClient();
            var response = await http.GetAsync("https://localhost:7107/api/ApiHome/ApiRegistration");
            if (response.IsSuccessStatusCode)
                return View();
            else
                return RedirectToAction("Error", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Error()
        {
            HttpContext.Session.Clear();
            var http = new HttpClient();
            var response = await http.GetAsync("https://localhost:7107/api/ApiHome/ApiError");
            if (response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<ErrorViewModel>(await response.Content.ReadAsStringAsync());
                return View(error);
            } 
            else
                return View();
        }
    }
}