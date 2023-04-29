namespace YCompany.Claims.Domain.InfrastructureInterfaces
{
    public interface IClaimsStorageService
    {
        Task<bool> CheckHealthAsync();
    }
}
