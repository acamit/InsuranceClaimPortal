using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YCompany.Payments.Domain.InfrastructureInterfaces;

namespace YCompany.Payments.DataAccess
{
    public class SqlStorageService : IPaymentsStorageService
    {
        public Task<bool> CheckHealthAsync()
        {
            return Task.FromResult(true);
        }
    }
}
