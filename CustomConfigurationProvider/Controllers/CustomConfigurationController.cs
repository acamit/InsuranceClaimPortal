using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YCompany.Configurations;

namespace CustomConfigurationProvider.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomConfigurationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public CustomConfigurationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var metadata = new SecurityMetaData
            {
                ApiKey = _configuration["ApiKey"],
                ApiSecret = _configuration["ApiSecret"]
            };
            return Ok(metadata);
        }
    }
}
