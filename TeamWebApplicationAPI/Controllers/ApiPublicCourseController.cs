using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TeamWebApplicationAPI.Data.ExceptionLogger;
using TeamWebApplicationAPI.Models;
using TeamWebApplicationAPI.Repositories.Interfaces;

namespace TeamWebApplicationAPI.Controllers
{
    [ApiController]
    [Route("api/ApiPublicCourse")]
    public class ApiPublicCourseController : ControllerBase
    {
        private readonly IDataLogger _logger;
        private readonly IMapper _mapper;
        private readonly IUsersRepository _usersRepository;
        private readonly ICoursesRepository _coursesRepository;

        public ApiPublicCourseController(IDataLogger logger, IMapper mapper,
            IUsersRepository usersRepository, ICoursesRepository coursesRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _usersRepository = usersRepository;
            _coursesRepository = coursesRepository;
        }

        [HttpGet("ApiIndex")]
        public async Task<IActionResult> ApiIndex([FromQuery] int? loggedInUserId, [FromQuery] string? searchString)
        {
            try
            {
                var publicCourses = await _coursesRepository.GetPublicCoursesAsync();
                if (!String.IsNullOrEmpty(searchString))
                {
                    publicCourses = publicCourses.Where(course => course.Name!.Contains(searchString, StringComparison.InvariantCultureIgnoreCase)).ToList();
                }

                var currentUser = await _usersRepository.GetUserByIdAsync(loggedInUserId);
                var viewModel = new CourseViewModel
                {
                    Courses = publicCourses,
                    User = currentUser
                };
                var viewModelDto = _mapper.Map<CourseViewModelDto>(viewModel);

                return Ok(viewModelDto);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return Unauthorized();
            }
        }
    }
}
