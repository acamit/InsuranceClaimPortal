
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YCompany.Configurations;
using YCompanyPaymentsAPI.Data;
using YCompanyPaymentsAPI.Models;

namespace YCompanyThirdPartyAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class ThirdPartyController : ControllerBase
    {
        private readonly InsuranceContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public ThirdPartyController(InsuranceContext context, IConfiguration configuration, ILogger logger)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
        }

        //[HttpGet]
        //public IEnumerable<Policy> Get()
        //{
        //    _logger.LogInformation(_configuration["ApiKey"]);
        //    List<Policy> result = _context.Policies.ToList();
        //    return result;
        //}
        [HttpGet]
        public IActionResult GetConfig()
        {
            var metadata = new SecurityMetadata
            {
                ApiKey = _configuration["ApiKey"],
                ApiSecret = _configuration["ApiSecret"]
            };
            return Ok(metadata);
        }
    }
}