using Microsoft.AspNetCore.Mvc;
using TeamWebApplication.Data;
using TeamWebApplication.Models;

namespace TeamWebApplication.Controllers
{
    public class PublicCourseController : Controller
    {
        private readonly ICourseContainer _courseContainer;

        public PublicCourseController(ICourseContainer courseContainer)
        {
            _courseContainer = courseContainer;
        }

        public IActionResult Index()
        {
            IEnumerable<Course> publicCourses = (
                from course in _courseContainer.courseList
                where course.IsPublic == true
                select course
            ).ToList();
            return View(publicCourses);
        }

        public IActionResult TeacherIndex()
        {
            IEnumerable<Course> publicCourses = (
                from course in _courseContainer.courseList
                where course.IsPublic == true
                select course
            ).ToList();
            return View(publicCourses);
        }
    }
}
