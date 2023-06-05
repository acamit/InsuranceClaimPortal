using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YCompany.HealthChecks.Interfaces;

namespace YCompany.Communications.Domain.InfrastructureInterfaces
{
    public interface ICommunicationStorageService : IStorageHealth
    {
        Task<bool> CheckHealthAsync();
    }
}
