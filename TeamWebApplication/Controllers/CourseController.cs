using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamWebApplication.Data;
using TeamWebApplication.Data.Database;
using TeamWebApplication.Models;

namespace TeamWebApplication.Controllers
{
    public class CourseController : Controller
    {
        private readonly ApplicationDBContext _db;

        public CourseController(ApplicationDBContext db)
        {
            _db = db;
        }


        public IActionResult Index()
        {
			_db.UserDetails.First<UserDetails>().currentCourseId = -1;
			IEnumerable<Course> coursesTaken = (
			    from user in _db.Users
				join userCourse in _db.CoursesUsers
				on user.UserId equals userCourse.UserId
				join course in _db.Courses
				on userCourse.CourseId equals course.CourseId
				where user.UserId == _db.UserDetails.First<UserDetails>().loggedInUserId
				select course
            ).ToList();
            
            User currentUser = _db.Users.Find(_db.UserDetails.First<UserDetails>().loggedInUserId);

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
			_db.CoursesUsers.Add(new CourseUser { CourseId = course.CourseId, UserId = _db.UserDetails.First<UserDetails>().loggedInUserId });
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
			_db.UserDetails.First<UserDetails>().currentCourseId = courseId;
			_db.SaveChanges();
			return View();
        }

        [HttpPost]
        public IActionResult AddUser(String userIdString)
        {
            String[] userIdList = userIdString.Split(';');
            Course? currentCourse = _db.Courses.Find(_db.UserDetails.First<UserDetails>().currentCourseId);
            foreach (var word in userIdList)
            {
                if (Int32.TryParse(word, out int userId) != false && currentCourse != null)
                {
                    User? user;
                    if ((user = _db.Users.Find(userId)) != null && userId != _db.UserDetails.First<UserDetails>().loggedInUserId)
                    {
                        _db.CoursesUsers.Add(new CourseUser { CourseId = _db.UserDetails.First<UserDetails>().currentCourseId, UserId = userId });
                        _db.SaveChanges();
                    }
                }
            }
            return RedirectToAction("Index");
        }

        public IActionResult RemoveUser(int courseId)
        {
			_db.UserDetails.First<UserDetails>().currentCourseId = courseId;
			_db.SaveChanges();
			return View();
        }

        [HttpPost]
        public IActionResult RemoveUser(String userIdString)
        {
            String[] userIdList = userIdString.Split(';');
            Course? currentCourse = _db.Courses.Find(_db.UserDetails.First<UserDetails>().currentCourseId);
            foreach (var word in userIdList)
            {
                if (Int32.TryParse(word, out int userId) != false && currentCourse != null)
                {
                    User? user;
                    if ((user = _db.Users.Find(userId)) != null && userId != _db.UserDetails.First<UserDetails>().loggedInUserId)
                    {
                        _db.CoursesUsers.Remove(new CourseUser { CourseId = _db.UserDetails.First<UserDetails>().currentCourseId, UserId = userId });
                        _db.SaveChanges();
                    }
                }
            }
            return RedirectToAction("Index");
        }

        public IActionResult CheckUsers(int courseId)
        {
			_db.UserDetails.First<UserDetails>().currentCourseId = courseId;
			_db.SaveChanges();
			ICollection<User> userInCourseList = (
				from user in _db.Users
				join userCourse in _db.CoursesUsers
				on user.UserId equals userCourse.UserId
				join course in _db.Courses
				on userCourse.CourseId equals course.CourseId
				where course.CourseId == _db.UserDetails.First<UserDetails>().currentCourseId
				select user
			).ToList();
            return View(userInCourseList);
        }
    }
}