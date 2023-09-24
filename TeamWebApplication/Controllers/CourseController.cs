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

        public IActionResult TeacherIndex()
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
            Course course = new Course();
            return View(course);
        }

        [HttpPost]
        public IActionResult Create(Course course)
        {
            int createdCourseId = _courseContainer.CreateCourse(course, _userContainer.loggedInUserId);
            _userContainer.AddRelation(_userContainer.loggedInUserId, createdCourseId);
            _relationContainer.AddRelationData(createdCourseId, _userContainer.loggedInUserId);
            _courseContainer.WriteCourses();
            return RedirectToAction("TeacherIndex");
        }

        public IActionResult Edit(int courseId)
        {
            Course course = _courseContainer.courseList.SingleOrDefault(course => course.Id == courseId);
            return View(course);
        }

        [HttpPost]
        public IActionResult Edit(Course course)
        {
            Course originalCourse = _courseContainer.courseList.SingleOrDefault(originalCourse => originalCourse.Id == course.Id);
            originalCourse.Name = course.Name;
            //So we ask to select faculty, but we do not store it in the course model???
            originalCourse.IsVisible = course.IsVisible;
            originalCourse.Description = course.Description;
            _courseContainer.WriteCourses();
            return RedirectToAction("TeacherIndex");
        }
    }
}