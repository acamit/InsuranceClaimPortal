using Microsoft.Extensions.Diagnostics.HealthChecks;
using YCompany.Claims.Domain.InfrastructureInterfaces;

namespace YCompanyClaimsAPI.HealthChecks
{
    public class StorageHealthChecks : IHealthCheck
    {
        private readonly IClaimsStorageService _storageService;

        public StorageHealthChecks(IClaimsStorageService storageService)
        {
            _storageService = storageService;
        }
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var isStorageOk = await _storageService.CheckHealthAsync();
            return isStorageOk ? HealthCheckResult.Healthy() : HealthCheckResult.Unhealthy();
        }
    }
}
