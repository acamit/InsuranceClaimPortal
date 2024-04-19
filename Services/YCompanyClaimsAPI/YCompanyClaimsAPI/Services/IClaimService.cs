using YCompanyClaimsAPI.Models;

namespace YCompanyClaimsAPI.Services
{
    public interface IClaimService
    {
        Task<List<ClaimModel>> GetClaimsAsync();
        Task<ClaimModel> GetClaimsByIdAsync(int id);
        Task<ClaimModel> AddClaimsAsync(ClaimModel claim);
        Task UpdateClaimsAsync(int id, ClaimModel claim);
        Task DeleteClaimsAsync(int id);

    }
}
