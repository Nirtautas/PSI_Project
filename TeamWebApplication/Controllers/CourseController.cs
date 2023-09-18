using Microsoft.AspNetCore.Mvc;
using TeamWebApplication.Data;
using TeamWebApplication.Models;

namespace TeamWebApplication.Controllers
{
    public class CourseController : Controller
    {
		private readonly IUserContainer _userContainer;//readonly - constant
        private readonly ICourseContainer _courseContainer;

		public CourseController(IUserContainer userContainer, ICourseContainer courseContainer)
		{
            _userContainer = userContainer;
            _courseContainer = courseContainer;
		}

		public IActionResult Index()
        {
			IEnumerable<Course> coursesTaken = (
				from user in _userContainer.userList
				where user.UserId == _userContainer.loggedInUserId
				from courseId in user.CoursesUserTakesId
				join course in _courseContainer.courseList on courseId equals course.Id
				select course
			).ToList();
			return View(coursesTaken);
        }

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
        public IActionResult Create(Course course)
        {
			_courseContainer.CreateCourse(course);
            return RedirectToAction("Index");
        }
    }
}
