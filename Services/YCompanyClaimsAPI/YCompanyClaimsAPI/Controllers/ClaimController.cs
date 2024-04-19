using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YCompanyClaimsAPI.Models;
using YCompanyClaimsAPI.Services;

namespace YCompanyClaimsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClaimController : ControllerBase
    {
        private readonly IClaimService _claimService;

        public ClaimController(IClaimService claimService)
        {
            _claimService = claimService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ClaimModel>>> GetClaims()
        {
            var claims = await _claimService.GetClaimsAsync();
            return Ok(claims);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClaimModel>> GetClaimById(int id)
        {
            var claim = await _claimService.GetClaimsByIdAsync(id);
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
            await _claimService.UpdateClaimsAsync(id, claim);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClaim(int id)
        {
            await _claimService.DeleteClaimsAsync(id);
            return NoContent();
        }
    }
}
