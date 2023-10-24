using Microsoft.AspNetCore.Mvc;
using TeamWebApplication.Data.Database;
using TeamWebApplication.Data.ExceptionLogger;
using TeamWebApplication.Data.Exceptions;
using TeamWebApplication.Data.ExtensionMethods;
using TeamWebApplication.Models;

namespace TeamWebApplication.Controllers
{
    public class CourseController : Controller
    {
        private readonly ApplicationDBContext _db;
        private readonly IExceptionLogger _logger;

        public CourseController(ApplicationDBContext db, IExceptionLogger logger)
        {
            _db = db;
            _logger = logger;
        }

        public IActionResult Index()
        {
            try
            {
				int? loggedInUserId = HttpContext.Session.GetInt32Ex("LoggedInUserId");
				HttpContext.Session.Remove("CurrentCourseId");

                string searchString = Request.Query["searchString"];
				IEnumerable<Course> coursesTaken = (
					from user in _db.Users
					join userCourse in _db.CoursesUsers
					on user.UserId equals userCourse.UserId
					join course in _db.Courses
					on userCourse.CourseId equals course.CourseId
					where user.UserId == loggedInUserId
					select course
				);
                if (!String.IsNullOrEmpty(searchString))
				{
                    coursesTaken = coursesTaken.Where(course => course.Name.Contains(searchString, StringComparison.InvariantCultureIgnoreCase)).ToList();
                }

				var currentUser = _db.Users.Find(loggedInUserId);

				var viewModel = new CourseViewModel
				{
					Courses = coursesTaken,
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

        public IActionResult Create()
        {
            try
            {
				HttpContext.Session.GetInt32Ex("LoggedInUserId");
				Course course = new Course();
				return View(course);
			}
            catch (SessionCredentialException ex)
            {
                _logger.Log(ex);
				return RedirectToAction("Index", "Home");
			}
        }

        [HttpPost]
        public IActionResult Create(Course course)
        {
            try
            {
				int? loggedInUserId = HttpContext.Session.GetInt32Ex("LoggedInUserId");
				course.CreationDate = DateTime.Now;
				_db.Courses.Add(course);
				_db.SaveChanges();
				_db.CoursesUsers.Add(new CourseUser { CourseId = course.CourseId, UserId = (int)loggedInUserId });
				_db.SaveChanges();
				return RedirectToAction("Index");
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

        public IActionResult Edit(int courseId)
        {
            try
            {
				HttpContext.Session.GetInt32Ex("LoggedInUserId");
				Course? course = _db.Courses.Find(courseId);
				return View(course);
			}
            catch (SessionCredentialException ex)
            {
                _logger.Log(ex);
                return RedirectToAction("Index", "Home");
            }
		}

        [HttpPost]
        public IActionResult Edit(Course course)
        {
            try
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
            catch (Exception ex)
            {
                _logger.Log(ex);
                throw;
            }
        }

        public IActionResult Delete(int courseId)
        {
            try
            {
                HttpContext.Session.GetInt32Ex("LoggedInUserId");
                Course? course = _db.Courses.Find(courseId);
                return View(course);
            }
            catch (SessionCredentialException ex)
            {
				_logger.Log(ex);
				return RedirectToAction("Index", "Home");
			}
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteCourse(int courseId)
        {
            try
            {
                Course? course = _db.Courses.Find(courseId);
                _db.Courses.Remove(course);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                throw;
            }
        }

        public IActionResult AddUser(int courseId)
        {
            try
            {
				HttpContext.Session.GetInt32Ex("LoggedInUserId");
				HttpContext.Session.SetInt32("CurrentCourseId", courseId);
				_db.SaveChanges();
				return View();
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

        [HttpPost]
        public IActionResult AddUser(String userIdString)
        {
            try
            {
				int? loggedInUserId = HttpContext.Session.GetInt32Ex("LoggedInUserId");
				int? currentCourseId = HttpContext.Session.GetInt32Ex("CurrentCourseId");
				String[] userIdList = userIdString.Split(';');
				Course? currentCourse = _db.Courses.Find(currentCourseId);
				foreach (var word in userIdList)
				{
					if (Int32.TryParse(word, out int userId) != false && currentCourse != null)
					{
						User? user;
						if ((user = _db.Users.Find(userId)) != null && userId != loggedInUserId)
						{
							_db.CoursesUsers.Add(new CourseUser { CourseId = (int)currentCourseId, UserId = userId });
							_db.SaveChanges();
						}
					}
				}
				return RedirectToAction("Index");
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

        public IActionResult RemoveUser(int courseId)
        {
            try
            {
				HttpContext.Session.GetInt32Ex("LoggedInUserId");
				HttpContext.Session.SetInt32("CurrentCourseId", courseId);
				return View();
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

        [HttpPost]
        public IActionResult RemoveUser(String userIdString)
        {	
			try
			{
				int? loggedInUserId = HttpContext.Session.GetInt32Ex("LoggedInUserId");
				int? currentCourseId = HttpContext.Session.GetInt32Ex("CurrentCourseId");
				String[] userIdList = userIdString.Split(';');
				Course? currentCourse = _db.Courses.Find(currentCourseId);
				foreach (var word in userIdList)
				{
					if (Int32.TryParse(word, out int userId) != false && currentCourse != null)
					{
						User? user;
						if ((user = _db.Users.Find(userId)) != null && userId != loggedInUserId)
						{
							_db.CoursesUsers.Remove(new CourseUser { CourseId = (int)currentCourseId, UserId = userId });
							_db.SaveChanges();
						}
					}
				}
				return RedirectToAction("Index");
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

        public IActionResult CheckUsers(int courseId)
        {
			try
			{
				HttpContext.Session.SetInt32("CurrentCourseId", courseId);
				int? currentCourseId = HttpContext.Session.GetInt32Ex("CurrentCourseId");
				_db.SaveChanges();
				ICollection<User> userInCourseList = (
					from user in _db.Users
					join userCourse in _db.CoursesUsers
					on user.UserId equals userCourse.UserId
					join course in _db.Courses
					on userCourse.CourseId equals course.CourseId
					where course.CourseId == currentCourseId
					select user
				).ToList();
				return View(userInCourseList);
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