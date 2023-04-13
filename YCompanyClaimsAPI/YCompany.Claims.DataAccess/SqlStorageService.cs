using YCompany.Claims.Domain.InfrastructureInterfaces;

namespace YCompany.Claims.DataAccess
{
    internal class SqlStorageService : IClaimsStorageService
    {
        public Task<bool> CheckHealthAsync()
        {
            return Task.FromResult(true);
        }
    }
}
