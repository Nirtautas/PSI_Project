using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using TeamWebApplicationAPI.Data.ExceptionLogger;
using TeamWebApplicationAPI.Models;
using TeamWebApplicationAPI.Repositories.Interfaces;

namespace TeamWebApplicationAPI.Controllers
{
    [ApiController]
    [Route("api/ApiLogIn")]
    public class ApiLogInController : Controller
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IDataLogger _logger;

        public ApiLogInController(IDataLogger logger, IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
            _logger = logger;
        }

        [HttpGet("ApiGetLoginObject")]
        public IActionResult ApiGetLoginObject()
        {
            var loginDetails = JsonConvert.SerializeObject(new LoginDetails());
            return Ok(loginDetails);
        }

        [HttpGet("ApiRegistration")]
        public IActionResult ApiRegistration()
        {
            return Ok();
        }

        
        [HttpPost("ApiLogin")]
        public async Task<IActionResult> ApiLogin([FromBody] LoginDetails login)
        {
            try
            {
                var user = await _usersRepository.GetUserByCredentialsAsync(login.UserId, login.Password);
                if (user == null)
                    return Unauthorized();

                return Ok(user);
            }
            catch (Exception ex1)
            {
                _logger.Log(ex1);
                throw;
            }
        }
    }
}