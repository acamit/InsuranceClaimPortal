using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YCompany.HealthChecks.Interfaces;

namespace YCompany.Reporting.Domain.InfrastructureInterfaces
{
    public interface IReportingStorageService : IStorageHealth
    {
        Task<bool> CheckHealthAsync();
    }
}
