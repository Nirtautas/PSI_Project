using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using TeamWebApplicationAPI.Models;

namespace TeamWebApplicationAPI.Controllers
{
    [ApiController]
    [Route("api/ApiHome")]
    public class ApiHomeController : ControllerBase
    {
        [HttpGet("ApiIndex")]
        public IActionResult ApiIndex()
        {
            return Ok();
        }

        [HttpGet("ApiRegistration")]
        public IActionResult ApiRegistration()
        {
            return Ok();
        }

        [HttpGet("ApiError")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult ApiError()
        {
            var error = JsonConvert.SerializeObject(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            return Ok(error);
        }
    }
}
