using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YCompany.Configurations;
using YCompanyPaymentsAPI.Data;
using YCompanyPaymentsAPI.Models;

namespace YCompanyThirdPartyAPI.Controllers
{
    [ApiController]
   // [Authorize]
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

        [HttpGet("/allPolicies")]
        public IEnumerable<Policy> GetAllPolicies()
        {
            List<Policy> result = _context.Policies.ToList();
            return result;
        }

        [HttpGet("/policiesById/{id}")]
        public async Task<IActionResult> GetMyPolicies(int id)
        {
            var myPolicies = await (
                from policies in _context.Policies
                join vehicle in _context.Vehicles on policies.Id equals vehicle.PolicyId
                where policies.Id == id
                select new
                {
                    policies.Id,
                    policies.PolicyName,
                    policies.PolicyNumber,
                    policies.PolicyExpirationDate,
                    policies.Active,
                    policies.CreatedDate,
                    vehicle.VehicleYear,
                    vehicle.Model,
                    vehicle.Color,
                    vehicle.VehicleNumberPlate,
                }
                ).ToListAsync();
            return Ok(myPolicies);
        }

        //[HttpGet]
        //public IActionResult GetValue()
        //{
        //    var metadata = new SecretManagerConfigurationSecurityMetadata
        //    {
        //        Key1 = _configuration["Key1"],
        //        Key2 = _configuration["Key2"]
        //    };
        //    Console.WriteLine(metadata);
        //    return Ok(metadata);
        //}
    }
}