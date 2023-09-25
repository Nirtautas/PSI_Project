using Microsoft.AspNetCore.Mvc;
using TeamWebApplication.Data;
using TeamWebApplication.Models;

namespace TeamWebApplication.Controllers
{
    public class CourseController : Controller
    {
        private readonly IUserContainer _userContainer;//readonly - constant
        private readonly ICourseContainer _courseContainer;
        private readonly IRelationContainer _relationContainer;

        public CourseController(IUserContainer userContainer, ICourseContainer courseContainer, IRelationContainer relationContainer)
        {
            _userContainer = userContainer;
            _courseContainer = courseContainer;
            _relationContainer = relationContainer;
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
            int createdCourseId = _courseContainer.CreateCourse(course, _userContainer.loggedInUserId);
            _userContainer.AddRelation(_userContainer.loggedInUserId, createdCourseId);
            _relationContainer.AddRelationData(createdCourseId, _userContainer.loggedInUserId);
            return RedirectToAction("Index");
        }

        public IActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Delete(Course course)
        {
            int deletedCourseId = _courseContainer.DeleteCourse(course);
            _userContainer.DeleteRelation(_userContainer.loggedInUserId, deletedCourseId);
            _relationContainer.DeleteRelationData(deletedCourseId, _userContainer.loggedInUserId);
            return RedirectToAction("Index");
        }
    }
}