using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;
using System.Threading;
using TeamWebApplication.Controllers.ControllerEventArgs;
using TeamWebApplication.Data.Database;
using TeamWebApplication.Data.ExceptionLogger;
using TeamWebApplication.Data.Exceptions;
using TeamWebApplication.Data.ExtensionMethods;
using TeamWebApplication.Data.MailService;
using TeamWebApplication.Models;
using TeamWebApplication.Repositories.Interfaces;

namespace TeamWebApplication.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICoursesRepository _coursesRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly ICourseUsersRepository _courseUserRepository;
        private readonly IDataLogger _logger;
        private readonly IMailService _mailService;

        // limits the amount of threads that can access a resource or pool of resources concurrently.
        private static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);
        private static ConcurrentQueue<int> userQueue = new ConcurrentQueue<int>();

        public event EventHandler<AttendanceEventArgs> Attendance;

        public CourseController(IDataLogger logger, IMailService mailService,
            ICoursesRepository coursesRepository, IUsersRepository usersRepository, ICourseUsersRepository courseUserRepository)
        {
            _logger = logger;
            _mailService = mailService;
            _coursesRepository = coursesRepository;
            _usersRepository = usersRepository;
            _courseUserRepository = courseUserRepository;
            Attendance = _mailService.OnAttendanceChange;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var loggedInUserId = HttpContext.Session.GetInt32Ex("LoggedInUserId");
                HttpContext.Session.Remove("CurrentCourseId");

                var searchString = Request.Query["searchString"];
                var coursesTaken = await _coursesRepository.GetCoursesByUserIdAsync(loggedInUserId);
                if (!String.IsNullOrEmpty(searchString))
                {
                    coursesTaken = coursesTaken.Where(course => course.Name.Contains(searchString, StringComparison.InvariantCultureIgnoreCase)).ToList();
                }

                var currentUser = await _usersRepository.GetUserByIdAsync(loggedInUserId);

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
        public async Task<IActionResult> Create(Course course)
        {
            try
            {
                int? loggedInUserId = HttpContext.Session.GetInt32Ex("LoggedInUserId");
                course.CreationDate = DateTime.Now;
                await _coursesRepository.InsertCourseAsync(course);
                await _courseUserRepository.InsertRelationAsync(course.CourseId, loggedInUserId);
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

        public async Task<IActionResult> Edit(int courseId)
        {
            try
            {
                HttpContext.Session.GetInt32Ex("LoggedInUserId");
                Course? course = await _coursesRepository.GetCourseByIdAsync(courseId);
                return View(course);
            }
            catch (SessionCredentialException ex)
            {
                _logger.Log(ex);
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Course course)
        {
            try
            {
                userQueue.Enqueue((int)HttpContext.Session.GetInt32Ex("LoggedInUserId"));
                semaphoreSlim.Wait();
                if (userQueue.TryDequeue(out int queuedUserId))
                {
                    if (queuedUserId == (int)HttpContext.Session.GetInt32Ex("LoggedInUserId"))
                    {
                        await _coursesRepository.UpdateCourseAsync(course);
                    }
                }
                semaphoreSlim.Release();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                throw;
            }
        }

        public async Task<IActionResult> Delete(int courseId)
        {
            try
            {
                HttpContext.Session.GetInt32Ex("LoggedInUserId");
                Course? course = await _coursesRepository.GetCourseByIdAsync(courseId);
                return View(course);
            }
            catch (SessionCredentialException ex)
            {
                _logger.Log(ex);
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteCourse(int courseId)
        {
            try
            {
                await _coursesRepository.DeleteCourseByIdAsync(courseId);
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
        public async Task<IActionResult> AddUser(string userIdString)
        {
            try
            {
                int? loggedInUserId = HttpContext.Session.GetInt32Ex("LoggedInUserId");
                int? currentCourseId = HttpContext.Session.GetInt32Ex("CurrentCourseId");
                userQueue.Enqueue((int)loggedInUserId);
                semaphoreSlim.Wait();
                if (userQueue.TryDequeue(out int queuedUserId))
                {
                    if (queuedUserId == loggedInUserId)
                    {
                        String[] userIdList = userIdString.Split(';');
                        Course? currentCourse = await _coursesRepository.GetCourseByIdAsync(currentCourseId);
                        foreach (var word in userIdList)
                        {
                            if (Int32.TryParse(word, out int userId) != false && currentCourse != null)
                            {
                                User? user;
                                if ((user = await _usersRepository.GetUserByIdAsync(userId)) != null && userId != loggedInUserId && 
                                    !await _courseUserRepository.CheckIfRelationExistsAsync(currentCourse.CourseId, userId))
                                {
                                    await _courseUserRepository.InsertRelationAsync(currentCourseId, userId);
                                    OnAttendanceChange(user, currentCourse, true);
                                }
                            }
                        }
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
                semaphoreSlim.Release();
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
        public async Task<IActionResult> RemoveUser(String userIdString)
        {
            try
            {
                int? loggedInUserId = HttpContext.Session.GetInt32Ex("LoggedInUserId");
                int? currentCourseId = HttpContext.Session.GetInt32Ex("CurrentCourseId");
                String[] userIdList = userIdString.Split(';');
                Course? currentCourse = await _coursesRepository.GetCourseByIdAsync(currentCourseId);
                userQueue.Enqueue((int)loggedInUserId);
                semaphoreSlim.Wait();
                if (userQueue.TryDequeue(out int queuedUserId))
                {
                    if (queuedUserId == currentCourseId)
                    {
                        foreach (var word in userIdList)
                        {
                            if (Int32.TryParse(word, out int userId) != false && currentCourse != null)
                            {
                                User? user;
                                if ((user = await _usersRepository.GetUserByIdAsync(userId)) != null && userId != loggedInUserId)
                                {
                                    await _courseUserRepository.DeleteRelationAsync(currentCourseId, userId);
                                    OnAttendanceChange(user, currentCourse, false);
                                }
                            }
                        }
                    }
                }
                semaphoreSlim.Release(); 
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

        public async Task<IActionResult> CheckUsers(int courseId)
        {
            try
            {
                HttpContext.Session.SetInt32("CurrentCourseId", courseId);
                int? currentCourseId = HttpContext.Session.GetInt32Ex("CurrentCourseId");
                var userInCourseList = await _usersRepository.GetUsersInCourseAsync(currentCourseId);
                return View(userInCourseList);
            }
            catch (SessionCredentialException ex)
            {
                _logger.Log(ex);
                //would be nice if it explained what its catching instead of just SessionCredentialException :((
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex1)
            {
                //same here
                _logger.Log(ex1);
                throw;
            }
        }

        protected virtual void OnAttendanceChange(User user, Course course, bool addedOrRemoved)
        {
            if (Attendance != null)
            {
                if (addedOrRemoved)
                    _logger.Log(DateTime.Now + $": Added {user.Name} to course {course.Name}.");
                else
                    _logger.Log(DateTime.Now + $": Removed {user.Name} from course {course.Name}.");
                Attendance.Invoke(this, new AttendanceEventArgs(user, course, addedOrRemoved));
            }
        }
    }
}