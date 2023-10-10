using Microsoft.AspNetCore.Mvc;
using TeamWebApplication.Data;
using TeamWebApplication.Data.Database;
using TeamWebApplication.Models;

namespace TeamWebApplication.Controllers
{
    public class PublicCourseController : Controller
    {
        private readonly IUserContainer _userContainer;
        private readonly ApplicationDBContext _db;

        public PublicCourseController(IUserContainer userContainer, ApplicationDBContext db)
        {
            _userContainer = userContainer;
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Course> publicCourses = (
                from course in _db.Courses
                where course.IsPublic == true
                select course
            ).ToList();
            var currentUser = _db.Users.Find(_userContainer.loggedInUserId);

            var viewModel = new CourseViewModel
            {
                Courses = publicCourses,
                User = currentUser
            };

            return View(viewModel);
        }

        public IActionResult TeacherIndex()
        {
            IEnumerable<Course> publicCourses = (
                from course in _db.Courses
                where course.IsPublic == true
                select course
            ).ToList();
            return View(publicCourses);
        }
    }
}
