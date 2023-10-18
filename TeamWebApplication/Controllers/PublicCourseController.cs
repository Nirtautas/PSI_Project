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
        private readonly IExceptionLogger _logger;

        public PublicCourseController(ApplicationDBContext db, IExceptionLogger logger)
        {
            _db = db;
            _logger = logger;
        }

        public IActionResult Index()
        {
            try
            {
				var currentUser = _db.Users.Find(HttpContext.Session.GetInt32Ex("LoggedInUserId"));

				IEnumerable<Course> publicCourses = (
                    from course in _db.Courses
                    where course.IsPublic == true
                    select course
                ).ToList();

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
				IEnumerable<Course> publicCourses = (
		            from course in _db.Courses
		            where course.IsPublic == true
		            select course
	            ).ToList();
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
