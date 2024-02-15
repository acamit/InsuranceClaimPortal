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

        public ThirdPartyController(InsuranceContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet]
        public IEnumerable<Policy> Get()
        {
            List<Policy> result = _context.Policies.ToList();
            return result;
        }

        [HttpGet]
        public IActionResult GetValue()
        {
            var metadata = new SecretManagerConfigurationSecurityMetadata
            {
                Key1 = _configuration["Key1"],
                Key2 = _configuration["Key2"]
            };
            Console.WriteLine(metadata);
            return Ok(metadata);
        }
    }
}