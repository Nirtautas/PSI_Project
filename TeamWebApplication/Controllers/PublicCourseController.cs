using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using TeamWebApplicationAPI.Data.ExtensionMethods;
using TeamWebApplicationAPI.Models;

namespace TeamWebApplication.Controllers
{
    public class PublicCourseController : Controller
    {
        private readonly IMapper _mapper;

        public PublicCourseController(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            int? loggedInUserId = HttpContext.Session.GetInt32Ex("LoggedInUserId");
            HttpContext.Session.Remove("CurrentCourseId");
            string? searchString = Request.Query["searchString"];

            var http = new HttpClient();
            var response = await http.GetAsync($"https://localhost:7107/api/ApiPublicCourse/ApiIndex?loggedInUserId={loggedInUserId}&searchString={searchString}");
            if (response.IsSuccessStatusCode)
            {
                var viewModelDto = JsonConvert.DeserializeObject<CourseViewModelDto>(await response.Content.ReadAsStringAsync());
                var viewModel = _mapper.Map<CourseViewModel>(viewModelDto);
                return View(viewModel);
            }
            else
                return RedirectToAction("Error", "Home");
        }
    }
}
