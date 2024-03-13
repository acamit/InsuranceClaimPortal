using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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
        private readonly MyApiCredentials _myApiCredentials;


        public ThirdPartyController(InsuranceContext context, IOptions<MyApiCredentials> options)
        {
            _context = context;
            _myApiCredentials = options.Value;
        }

        //[HttpGet]
        //public IEnumerable<Policy> Get()
        //{
        //    List<Policy> result = _context.Policies.ToList();
        //    return result;
        //}

        [HttpGet]
        public IActionResult GetKey()
        {
            var metadata = new MyApiCredentials
            {
                ApiKey = _myApiCredentials.ApiKey,
                UserId = _myApiCredentials.UserId
            };
            return Ok(metadata);
        }
    }
}