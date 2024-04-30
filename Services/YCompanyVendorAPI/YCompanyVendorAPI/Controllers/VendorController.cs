using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YCompany.Vendor.DataAccess;
using YCompany.Vendor.Domain.Entities;

namespace YCompanyVendorAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        private readonly RepositoryDbContext _context;

        public VendorController(RepositoryDbContext context)
        {
            _context = context;
        }

        [HttpGet("/allVendors")]
        public IEnumerable<Vendors> GetAllVendors()
        {
            List<Vendors> result = _context.Vendors.ToList();
            return result;
        }
    }
}
