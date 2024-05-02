using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YCompany.Claims.DataAccess;
using YCompanyClaimsAPI.Models;
using YCompanyClaimsAPI.Services;

namespace YCompanyClaimsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClaimController : ControllerBase
    {
        private readonly IClaimService _claimService;
        private readonly RepositoryDbContext _dbContext;

        public ClaimController(IClaimService claimService)
        {
            _claimService = claimService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ClaimModel>>> GetClaims()
        {
            var claims = await _dbContext.ClientClaims.ToListAsync();
            return Ok(claims);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClaimModel>> GetClaimById(int id)
        {
            var claim = await _dbContext.ClientClaims.FindAsync(id);
            if (claim == null)
            {
                return NotFound();
            }
            return Ok(claim);
        }

        [HttpPost]
        public async Task<ActionResult<ClaimModel>> AddClaim(ClaimModel claim)
        {
            var addedClaim = await _claimService.AddClaimsAsync(claim);
            return CreatedAtAction(nameof(GetClaimById), new { id = addedClaim.Id }, addedClaim);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClaim(int id, ClaimModel claim)
        {
            if (id != claim.Id)
            {
                return BadRequest();
            }

            _dbContext.Entry(claim).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClaimsAsync(int id)
        {
            var claimToRemove = await _dbContext.ClientClaims.FindAsync(id);
            if (claimToRemove == null)
            {
                return NotFound();
            }

            _dbContext.ClientClaims.Remove(claimToRemove);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

    }
}
