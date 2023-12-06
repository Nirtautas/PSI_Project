using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TeamWebApplicationAPI.Data.ExceptionLogger;
using TeamWebApplicationAPI.Models;
using TeamWebApplicationAPI.Repositories.Interfaces;

namespace TeamWebApplicationAPI.Controllers
{
    [ApiController]
    [Route("api/ApiCourse")]
    public class ApiCourseController : ControllerBase
    {
        private readonly IDataLogger _logger;
        private readonly IMapper _mapper;
        private readonly ICoursesRepository _coursesRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly ICourseUsersRepository _courseUserRepository;

        public ApiCourseController(IDataLogger logger, IMapper mapper,
            ICoursesRepository coursesRepository, IUsersRepository usersRepository, ICourseUsersRepository courseUserRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _coursesRepository = coursesRepository;
            _usersRepository = usersRepository;
            _courseUserRepository = courseUserRepository;
        }

        [HttpGet("ApiIndex")]
        public async Task<IActionResult> ApiIndex([FromQuery] int? loggedInUserId, [FromQuery] string? searchString)
        {
            try
            {
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
                var viewModelDto = _mapper.Map<CourseViewModelDto>(viewModel);

                return Ok(viewModelDto);
            }
            catch (Exception ex1)
            {
                _logger.Log(ex1);
                return Unauthorized();
            }
        }

        [HttpGet("ApiCreate")]
        public IActionResult ApiCreate()
        {
            var map = _mapper.Map<CourseDto>(new Course());
            return Ok(map);
        }

        [HttpPost("ApiCreate")]
        public async Task<IActionResult> ApiCreate([FromBody] CourseDto courseDto, [FromQuery] int? loggedInUserId)
        {
            try
            {
                var course = _mapper.Map<Course>(courseDto);
                course.CreationDate = DateTime.Now;
                await _coursesRepository.InsertCourseAsync(course);
                await _courseUserRepository.InsertRelationAsync(course.CourseId, loggedInUserId);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return Unauthorized();
            }
        }

        [HttpGet("ApiEdit")]
        public async Task<IActionResult> ApiEdit([FromQuery] int courseId)
        {
            try
            {
                var course = await _coursesRepository.GetCourseByIdAsync(courseId);
                var map = _mapper.Map<CourseDto>(course);
                return Ok(map);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return Unauthorized();
            }
        }

        [HttpPut("ApiEdit")]
        public async Task<IActionResult> ApiEdit([FromBody] CourseDto courseDto)
        {
            try
            {
                var course = _mapper.Map<Course>(courseDto);
                await _coursesRepository.UpdateCourseAsync(course);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return Unauthorized();
            }
        }

        [HttpGet("ApiDelete")]
        public async Task<IActionResult> ApiDelete([FromQuery] int courseId)
        {
            try
            {
                var course = await _coursesRepository.GetCourseByIdAsync(courseId);
                var map = _mapper.Map<CourseDto>(course);
                return Ok(map);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return Unauthorized();
            }
        }

        [HttpDelete("ApiDelete")]
        public async Task<IActionResult> ApiDeleteCourse([FromQuery] int courseId)
        {
            try
            {
                await _coursesRepository.DeleteCourseByIdAsync(courseId);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return Unauthorized();
            }
        }

        [HttpGet("ApiAddUser")]
        public IActionResult ApiAddUser([FromQuery] int courseId)
        {
            return Ok();
        }

        [HttpPost("ApiAddUser")]
        public async Task<IActionResult> ApiAddUser([FromQuery] int? currentCourseId, [FromQuery] int? loggedInUserId, [FromBody] string userIdString)
        {
            try
            {
                string[] userIdList = userIdString.Split(';');
                var currentCourse = await _coursesRepository.GetCourseByIdAsync(currentCourseId);
                foreach (var word in userIdList)
                {
                    if (Int32.TryParse(word, out int userId) != false && currentCourse != null)
                    {
                        var user = await _usersRepository.GetUserByIdAsync(userId);
                        if (user != null && userId != loggedInUserId && !await _courseUserRepository.CheckIfRelationExistsAsync(currentCourse.CourseId, userId))
                            await _courseUserRepository.InsertRelationAsync(currentCourseId, userId);
                    }
                }
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return Unauthorized();
            }
        }

        [HttpGet("ApiRemoveUser")]
        public IActionResult ApiRemoveUser([FromQuery] int courseId)
        {
            return Ok();
        }

        [HttpDelete("ApiRemoveUser")]
        public async Task<IActionResult> ApiRemoveUser([FromQuery] int? currentCourseId, [FromQuery] int? loggedInUserId, [FromQuery] string userIdString)
        {
            try
            {
                string[] userIdList = userIdString.Split(';');
                var currentCourse = await _coursesRepository.GetCourseByIdAsync(currentCourseId);
                foreach (var word in userIdList)
                {
                    if (Int32.TryParse(word, out int userId) != false && currentCourse != null)
                    {
                        var user = await _usersRepository.GetUserByIdAsync(userId);
                        if (user != null && userId != loggedInUserId)
                            await _courseUserRepository.DeleteRelationAsync(currentCourseId, userId);
                    }
                }
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return Unauthorized();
            }
        }

        [HttpGet("ApiCheckUsers")]
        public async Task<IActionResult> ApiCheckUsers([FromQuery] int? currentCourseId)
        {
            try
            {
                var userInCourseList = await _usersRepository.GetUsersInCourseAsync(currentCourseId);
                var list = JsonConvert.SerializeObject(userInCourseList);
                return Ok(userInCourseList);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return Unauthorized();
            }
        }
    }
}
