using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public UserController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet()]
        public string Get()
        {
            return "Hello Jenkins";
        }
    }
}
