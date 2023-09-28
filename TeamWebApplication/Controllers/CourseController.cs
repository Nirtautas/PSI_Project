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
            Course? course = _courseContainer.GetCourse(courseId);
            return View(course);
        }

        [HttpPost]
        public IActionResult Edit(Course course)
        {
            Course? originalCourse = _courseContainer.GetCourse(course.Id);
            originalCourse.Name = course.Name;
            //So we ask to select faculty, but we do not store it in the course model???
            originalCourse.IsVisible = course.IsVisible;
            originalCourse.Description = course.Description;
            _courseContainer.WriteCourses();
            return RedirectToAction("TeacherIndex");
        }

        public IActionResult Delete(int courseId)
        {
            Course course = _courseContainer.courseList.SingleOrDefault(course => course.Id == courseId);
            return View(course);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteCourse(int courseId)
        {
            Course course = _courseContainer.courseList.SingleOrDefault(course => course.Id == courseId);
            if (course == null)
            {
                return NotFound();
            }
            _courseContainer.DeleteCourse(course);
            _userContainer.DeleteRelation(_userContainer.loggedInUserId, courseId);
            _relationContainer.DeleteRelationData(courseId, _userContainer.loggedInUserId);
            _courseContainer.WriteCourses();
            return RedirectToAction("TeacherIndex");
        }

        public IActionResult AddUser(int courseId)
        {
            _courseContainer.currentCourseId = courseId;
            return View();
        }

        [HttpPost]
        public IActionResult AddUser(String userIdString)
        {
            String[] userIdList = userIdString.Split(';');
            Course? currentCourse = _courseContainer.GetCourse(_courseContainer.currentCourseId);
            foreach (var word in userIdList) {
                if (Int32.TryParse(word, out int userId) != false && currentCourse != null)
                {
                    User? user;
                    if ((user = _userContainer.GetUser(userId)) != null && userId != _userContainer.loggedInUserId)
                    {
                        _relationContainer.AddRelationData(_courseContainer.currentCourseId, userId);
                        currentCourse.UsersInCourseId.Add(userId);
                        user.CoursesUserTakesId.Add(_courseContainer.currentCourseId);
                    }
                }
            }
            return RedirectToAction("TeacherIndex");
        }
    }
}