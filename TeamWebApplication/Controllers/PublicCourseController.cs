using Microsoft.AspNetCore.Mvc;
using TeamWebApplication.Data;
using TeamWebApplication.Models;

namespace TeamWebApplication.Controllers
{
    public class PublicCourseController : Controller
    {
        private readonly ICourseContainer _courseContainer;
        private readonly IUserContainer _userContainer;

        public PublicCourseController(ICourseContainer courseContainer, IUserContainer userContainer)
        {
            _courseContainer = courseContainer;
            _userContainer = userContainer;
        }

        public IActionResult Index()
        {
            IEnumerable<Course> publicCourses = (
                from course in _courseContainer.CourseList
                where course.IsPublic == true
                select course
            ).ToList();
            User currentUser = _userContainer.GetUser(_userContainer.LoggedInUserId);

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
                from course in _courseContainer.CourseList
                where course.IsPublic == true
                select course
            ).ToList();
            return View(publicCourses);
        }
    }
}
