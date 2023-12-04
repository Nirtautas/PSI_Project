using Microsoft.AspNetCore.Mvc;
using TeamWebApplicationAPI.Data.ExceptionLogger;
using TeamWebApplicationAPI.Data.Exceptions;
using TeamWebApplicationAPI.Data.ExtensionMethods;
using TeamWebApplicationAPI.Models;
using TeamWebApplicationAPI.Repositories.Interfaces;

namespace TeamWebApplication.Controllers
{
    public class PublicCourseController : Controller
    {
        private readonly IDataLogger _logger;
        private readonly IUsersRepository _usersRepository;
        private readonly ICoursesRepository _coursesRepository;

        public PublicCourseController(IDataLogger logger, IUsersRepository usersRepository, ICoursesRepository coursesRepository)
        {
            _logger = logger;
            _usersRepository = usersRepository;
            _coursesRepository = coursesRepository;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var loggedInUserId = HttpContext.Session.GetInt32Ex("LoggedInUserId");
                var currentUser = await _usersRepository.GetUserByIdAsync(loggedInUserId);

                var searchString = Request.Query["searchString"];
                var publicCourses = await _coursesRepository.GetPublicCoursesAsync();
                if (!String.IsNullOrEmpty(searchString))
                {
                    publicCourses = publicCourses.Where(course => course.Name!.Contains(searchString, StringComparison.InvariantCultureIgnoreCase)).ToList();
                }

                var viewModel = new CourseViewModel
                {
                    Courses = publicCourses,
                    User = currentUser
                };

				return View(viewModel);
			}
            catch (SessionCredentialException ex)
            {
                _logger.Log(ex);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex1)
            {
                _logger.Log(ex1);
                throw;
			}
		}
    }
}
