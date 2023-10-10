using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamWebApplication.Data;
using TeamWebApplication.Data.Database;
using TeamWebApplication.Models;

namespace TeamWebApplication.Controllers
{
    public class CourseController : Controller
    {
        private readonly IUserContainer _userContainer;
        private readonly ApplicationDBContext _db;

        public CourseController(IUserContainer userContainer, ApplicationDBContext db)
        {
            _db = db;
            _userContainer = userContainer;
        }

        public IActionResult Index()
        {
			_userContainer.currentCourseId = 0;
			IEnumerable<Course> coursesTaken = (
			    from user in _db.Users
				join userCourse in _db.CoursesUsers
				on user.UserId equals userCourse.UserId
				join course in _db.Courses
				on userCourse.CourseId equals course.CourseId
				where user.UserId == _userContainer.loggedInUserId
				select course
            ).ToList();
            
            User currentUser = _db.Users.Find(_userContainer.loggedInUserId);

            var viewModel = new CourseViewModel
            {
                Courses = coursesTaken,
                User = currentUser
            };

            return View(viewModel);
        }

        public IActionResult Create()
        {
            Course course = new Course();
            return View(course);
        }

        [HttpPost]
        public IActionResult Create(Course course)
        {
			course.CreationDate = DateTime.Now;
			_db.Courses.Add(course);
			_db.SaveChanges();
			_db.CoursesUsers.Add(new CourseUser { CourseId = course.CourseId, UserId = _userContainer.loggedInUserId });
			_db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int courseId)
        {
            Course? course = _db.Courses.Find(courseId);
            return View(course);
        }

        [HttpPost]
        public IActionResult Edit(Course course)
        {
			Course? originalCourse = _db.Courses.Find(course.CourseId);
            originalCourse.Name = course.Name;
            originalCourse.IsVisible = course.IsVisible;
            originalCourse.IsPublic = course.IsPublic;
            originalCourse.Description = course.Description;
            _db.Update(originalCourse);
			_db.SaveChanges();
			return RedirectToAction("Index");
        }

        public IActionResult Delete(int courseId)
        {
            Course course = _db.Courses.Find(courseId);
            return View(course);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteCourse(int courseId)
        {
            Course course = _db.Courses.Find(courseId);
            _db.Courses.Remove(course);
			_db.SaveChanges();
			return RedirectToAction("Index");
        }

        public IActionResult AddUser(int courseId)
        {
            _userContainer.currentCourseId = courseId;
            return View();
        }

        [HttpPost]
        public IActionResult AddUser(String userIdString)
        {
            String[] userIdList = userIdString.Split(';');
            Course? currentCourse = _db.Courses.Find(_userContainer.currentCourseId);
            foreach (var word in userIdList)
            {
                if (Int32.TryParse(word, out int userId) != false && currentCourse != null)
                {
                    User? user;
                    if ((user = _db.Users.Find(userId)) != null && userId != _userContainer.loggedInUserId)
                    {
                        _db.CoursesUsers.Add(new CourseUser { CourseId = _userContainer.currentCourseId, UserId = userId });
                        _db.SaveChanges();
                    }
                }
            }
            return RedirectToAction("Index");
        }

        public IActionResult RemoveUser(int courseId)
        {
            _userContainer.currentCourseId = courseId;
            return View();
        }

        [HttpPost]
        public IActionResult RemoveUser(String userIdString)
        {
            String[] userIdList = userIdString.Split(';');
            Course? currentCourse = _db.Courses.Find(_userContainer.currentCourseId);
            foreach (var word in userIdList)
            {
                if (Int32.TryParse(word, out int userId) != false && currentCourse != null)
                {
                    User? user;
                    if ((user = _db.Users.Find(userId)) != null && userId != _userContainer.loggedInUserId)
                    {
                        _db.CoursesUsers.Remove(new CourseUser { CourseId = _userContainer.currentCourseId, UserId = userId });
                        _db.SaveChanges();
                    }
                }
            }
            return RedirectToAction("Index");
        }

        public IActionResult CheckUsers(int courseId)
        {
            _userContainer.currentCourseId = courseId;
            ICollection<User> userInCourseList = (
				from user in _db.Users
				join userCourse in _db.CoursesUsers
				on user.UserId equals userCourse.UserId
				join course in _db.Courses
				on userCourse.CourseId equals course.CourseId
				where course.CourseId == _userContainer.currentCourseId
				select user
			).ToList();
            return View(userInCourseList);
        }
    }
}