using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using TeamWebApplicationAPI.Data.ExtensionMethods;
using TeamWebApplicationAPI.Models;

namespace TeamWebApplication.Controllers
{
    public class CourseController : Controller
    {
        private readonly IMapper _mapper;

        public CourseController(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            //Getting session variables
            int? loggedInUserId = HttpContext.Session.GetInt32Ex("LoggedInUserId");
            HttpContext.Session.Remove("CurrentCourseId");
            string? searchString = Request.Query["searchString"];

            var http = new HttpClient();
            var response = await http.GetAsync($"https://localhost:7107/api/ApiCourse/ApiIndex?loggedInUserId={loggedInUserId}&searchString={searchString}");
            if (response.IsSuccessStatusCode)
            {
                var viewModelDto = JsonConvert.DeserializeObject<CourseViewModelDto>(await response.Content.ReadAsStringAsync());
                var viewModel = _mapper.Map<CourseViewModel>(viewModelDto);
                return View(viewModel);
            }
            else
                return RedirectToAction("Index", "LogIn");
        }

        public async Task<IActionResult> Create()
        {
            HttpContext.Session.GetInt32Ex("LoggedInUserId");
            var http = new HttpClient();
            var response = await http.GetAsync($"https://localhost:7107/api/ApiCourse/ApiCreate");
            if (response.IsSuccessStatusCode)
            {
                var courseDto = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());
                var course = _mapper.Map<Course>(courseDto);
                return View(course);
            }
            else
                return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Create(Course course)
        {
            int? loggedInUserId = HttpContext.Session.GetInt32Ex("LoggedInUserId");
            var http = new HttpClient();
            var map = _mapper.Map<CourseDto>(course);
            var content = new StringContent(JsonConvert.SerializeObject(map), Encoding.UTF8, "application/json");
            var response = await http.PostAsync($"https://localhost:7107/api/ApiCourse/ApiCreate?loggedInUserId={loggedInUserId}", content);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");
            else
                return RedirectToAction("Error", "Home");
        }

        public async Task<IActionResult> Edit(int courseId)
        {
            HttpContext.Session.GetInt32Ex("LoggedInUserId");
            var http = new HttpClient();
            var response = await http.GetAsync($"https://localhost:7107/api/ApiCourse/ApiEdit?courseId={courseId}");
            if (response.IsSuccessStatusCode)
            {
                var courseDto = JsonConvert.DeserializeObject<CourseDto?>(await response.Content.ReadAsStringAsync());
                var course = _mapper.Map<Course>(courseDto);
                return View(course);
            }
            else
                return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Course course)
        {
            int? loggedInUserId = HttpContext.Session.GetInt32Ex("LoggedInUserId");
            var http = new HttpClient();
            var map = _mapper.Map<CourseDto>(course);
            var content = new StringContent(JsonConvert.SerializeObject(map), Encoding.UTF8, "application/json");
            var response = await http.PutAsync($"https://localhost:7107/api/ApiCourse/ApiEdit", content);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");
            else
                return RedirectToAction("Error", "Home");
        }

        public async Task<IActionResult> Delete(int courseId)
        {
            HttpContext.Session.GetInt32Ex("LoggedInUserId");
            var http = new HttpClient();
            var response = await http.GetAsync($"https://localhost:7107/api/ApiCourse/ApiDelete?courseId={courseId}");
            if (response.IsSuccessStatusCode)
            {
                var courseDto = JsonConvert.DeserializeObject<CourseDto?>(await response.Content.ReadAsStringAsync());
                var course = _mapper.Map<Course>(courseDto);
                return View(course);
            }
            else
                return RedirectToAction("Error", "Home");
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteCourse(int courseId)
        {
            int? loggedInUserId = HttpContext.Session.GetInt32Ex("LoggedInUserId");
            var http = new HttpClient();
            var response = await http.DeleteAsync($"https://localhost:7107/api/ApiCourse/ApiDelete?courseId={courseId}");
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");
            else
                return RedirectToAction("Error", "Home");
        }

        public async Task<IActionResult> AddUser(int courseId)
        {
            HttpContext.Session.GetInt32Ex("LoggedInUserId");
            HttpContext.Session.SetInt32("CurrentCourseId", courseId);
            var http = new HttpClient();
            var response = await http.GetAsync($"https://localhost:7107/api/ApiCourse/ApiAddUser?courseId={courseId}");
            if (response.IsSuccessStatusCode)
                return View();
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(string userIdString)
        {
            var loggedInUserId = HttpContext.Session.GetInt32Ex("LoggedInUserId");
            var currentCourseId = HttpContext.Session.GetInt32Ex("CurrentCourseId");
            var http = new HttpClient();
            var content = new StringContent(JsonConvert.SerializeObject(userIdString), Encoding.UTF8, "application/json");
            var response = await http.PostAsync($"https://localhost:7107/api/ApiCourse/ApiAddUser?currentCourseId={currentCourseId}&loggedInUserId={loggedInUserId}", content);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");
            return RedirectToAction("Error", "Home");
        }

        public async Task<IActionResult> RemoveUser(int courseId)
        {
            HttpContext.Session.GetInt32Ex("LoggedInUserId");
            HttpContext.Session.SetInt32("CurrentCourseId", courseId);
            var http = new HttpClient();
            var response = await http.GetAsync($"https://localhost:7107/api/ApiCourse/ApiRemoveUser?courseId={courseId}");
            if (response.IsSuccessStatusCode)
                return View();
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveUser(string userIdString)
        {
            var loggedInUserId = HttpContext.Session.GetInt32Ex("LoggedInUserId");
            var currentCourseId = HttpContext.Session.GetInt32Ex("CurrentCourseId");
            var http = new HttpClient();
            var response = await http.DeleteAsync($"https://localhost:7107/api/ApiCourse/ApiRemoveUser?currentCourseId={currentCourseId}&loggedInUserId={loggedInUserId}&userIdString={userIdString}");
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");
            return RedirectToAction("Error", "Home");
        }

        public async Task<IActionResult> CheckUsers(int courseId)
        {
            HttpContext.Session.SetInt32("CurrentCourseId", courseId);
            var http = new HttpClient();
            var response = await http.GetAsync($"https://localhost:7107/api/ApiCourse/ApiCheckUsers?currentCourseId={(int?)courseId}");
            if (response.IsSuccessStatusCode)
            {
                var list = JsonConvert.DeserializeObject<IEnumerable<User>>(await response.Content.ReadAsStringAsync());
                return View(list);
            }
            return RedirectToAction("Error", "Home");
        }
    }
}