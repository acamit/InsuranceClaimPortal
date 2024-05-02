using YCompanyClaimsAPI.Models;

namespace YCompanyClaimsAPI.Services
{
    public class ClaimService : IClaimService
    {
        private readonly List<ClaimModel> _claims;

        public ClaimService()
        {
            _claims = new List<ClaimModel>();
        }
        public async Task<ClaimModel> AddClaimsAsync(ClaimModel claim)
        {
            claim.Id = _claims.Count + 1;
            _claims.Add(claim);
            return await Task.FromResult(claim);
        }

        public async Task DeleteClaimsAsync(int id)
        {
            var existingClaim = _claims.FirstOrDefault(c => c.Id == id);
            if (existingClaim != null)
            {
                _claims.Remove(existingClaim);
            }
            await Task.CompletedTask;
        }

        public async Task<List<ClaimModel>> GetClaimsAsync()
        {
            return await Task.FromResult(_claims);
        }

        public async Task<ClaimModel> GetClaimsByIdAsync(int id)
        {
            return await Task.FromResult(_claims.FirstOrDefault(c => c.Id == id));
        }

        public async Task UpdateClaimsAsync(int id, ClaimModel claim)
        {
            var existingClaim = _claims.FirstOrDefault(c => c.Id == id);
            if (existingClaim != null)
            {
                existingClaim.Type = claim.Type;
                existingClaim.Value = claim.Value;
                existingClaim.ClientId = claim.ClientId;
            }
            await Task.CompletedTask;
        }
    }
}
