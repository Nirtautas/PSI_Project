using Microsoft.AspNetCore.Mvc;
using TeamWebApplication.Data.Database;
using TeamWebApplication.Data.ExceptionLogger;
using TeamWebApplication.Data.Exceptions;
using TeamWebApplication.Data.ExtensionMethods;
using TeamWebApplication.Models;

namespace TeamWebApplication.Controllers
{
    public class PublicCourseController : Controller
    {
        private readonly ApplicationDBContext _db;
        private readonly IDataLogger _logger;

        public PublicCourseController(ApplicationDBContext db, IDataLogger logger)
        {
            _db = db;
            _logger = logger;
        }

        public IActionResult Index()
        {
            try
            {
				var currentUser = _db.Users.Find(HttpContext.Session.GetInt32Ex("LoggedInUserId"));

                string searchString = Request.Query["searchString"];
                IEnumerable<Course> publicCourses = (
                    from course in _db.Courses
                    where course.IsPublic == true
                    select course
                );
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

        public IActionResult TeacherIndex()
        {
            try
            {
                HttpContext.Session.GetInt32Ex("LoggedInUserId");

                string searchString = Request.Query["searchString"];
                IEnumerable<Course> publicCourses = (
                    from course in _db.Courses
                    where course.IsPublic == true
                    select course
                );
                if (!String.IsNullOrEmpty(searchString))
                {
                    publicCourses = publicCourses.Where(course => course.Name!.Contains(searchString, StringComparison.InvariantCultureIgnoreCase)).ToList();
                }

                return View(publicCourses);
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
