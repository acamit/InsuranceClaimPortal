using Microsoft.Extensions.Diagnostics.HealthChecks;
using YCompany.Claims.Domain.InfrastructureInterfaces;

namespace YCompanyClaimsAPI.HealthChecks
{
    public class QueueHealthChecks : IHealthCheck
    {
        private readonly IMessageBroker _messageBroker;

        public QueueHealthChecks(IMessageBroker messageBroker)
        {
            _messageBroker = messageBroker;
        }
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var isStorageOk = await _messageBroker.CheckHealthAsync();
            return isStorageOk ? HealthCheckResult.Healthy() : HealthCheckResult.Unhealthy();
        }
    }
}
