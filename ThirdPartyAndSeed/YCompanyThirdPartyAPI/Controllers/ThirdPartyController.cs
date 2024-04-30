using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
//using YCompany.Configurations;
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
        //private readonly SecretManagerConfigurationData _secretManagerConfigurationData;

        /*public ThirdPartyController(InsuranceContext context, IOptions<SecretManagerConfigurationData> options)
        {
            _context = context;
            _secretManagerConfigurationData = options.Value;
        }*/

        /*public IActionResult GetKeyCredential()
        {
            var data = new SecretManagerConfigurationData
            {
                Key = _secretManagerConfigurationData.Key,
                UserId = _secretManagerConfigurationData.UserId
            };
            return Ok(data);
        }*/

        [HttpGet]
        public IEnumerable<Policy> Get()
        {
            List<Policy> result = _context.Policies.ToList();
            return result;
        }
    }
}