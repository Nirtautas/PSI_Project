using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using TeamWebApplicationAPI.Models;

namespace TeamWebApplication.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly IMapper _mapper;

        public RegistrationController(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var http = new HttpClient();
            var response = await http.GetAsync("https://localhost:7107/api/ApiRegistration/ApiIndex");
            if (response.IsSuccessStatusCode)
            {
                var user = JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync());
                return View(user);
            }
            return RedirectToAction("Error", "Home");
        }

        public async Task<IActionResult> Login(User user)
        {
            var userDto = _mapper.Map<UserDto>(user);
            var http = new HttpClient();
            var content = new StringContent(JsonConvert.SerializeObject(userDto), Encoding.UTF8, "application/json");
            var response = await http.PostAsync("https://localhost:7107/api/ApiRegistration/ApiLogin", content);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index", "LogIn");
            return RedirectToAction("Index", "Registration");
        }
    }
}
