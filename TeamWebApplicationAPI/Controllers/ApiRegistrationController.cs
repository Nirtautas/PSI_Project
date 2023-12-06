using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TeamWebApplicationAPI.Data.ExceptionLogger;
using TeamWebApplicationAPI.Models;
using TeamWebApplicationAPI.Repositories.Interfaces;

namespace TeamWebApplicationAPI.Controllers
{
    [ApiController]
    [Route("api/ApiRegistration")]
    public class ApiRegistrationController : ControllerBase
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IDataLogger _logger;
        private readonly IMapper _mapper;

        public ApiRegistrationController(IMapper mapper, IDataLogger logger, IUsersRepository usersRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _usersRepository = usersRepository;
        }

        [HttpGet("ApiIndex")]
        public IActionResult ApiIndex()
        {
            var user = JsonConvert.SerializeObject(new User());
            return Ok(user);
        }

        [HttpPost("ApiLogin")]
        public async Task<IActionResult> ApiLogin([FromBody] UserDto userDto)
        {
            try
            {
                var user = _mapper.Map<User>(userDto);
                if (!await _usersRepository.UserWithSuchEmailExistsAsync(user.Email))
                {
                    await _usersRepository.InsertUserAsync(user);
                    return Ok();
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                throw;
            }
        }
    }
}
