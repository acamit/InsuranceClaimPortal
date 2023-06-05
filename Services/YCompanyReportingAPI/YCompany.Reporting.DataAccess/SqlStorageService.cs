using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YCompany.Reporting.Domain.InfrastructureInterfaces;

namespace YCompany.Reporting.DataAccess
{
    public class SqlStorageService : IReportingStorageService
    {
        public Task<bool> CheckHealthAsync()
        {
            return Task.FromResult(true);
        }
    }
}
