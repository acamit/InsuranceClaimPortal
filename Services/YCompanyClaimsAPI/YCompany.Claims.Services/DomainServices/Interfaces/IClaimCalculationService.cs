using YCompany.Claims.Domain.Entities;

namespace YCompany.Claims.Domain.DomainServices.Interfaces
{
    public interface IClaimCalculationService
    {
        decimal CalculateClaimAmount(Claim claim);
        bool IsClaimEligible(Claim claim);
    }
}
