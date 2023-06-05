using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YCompany.Vendor.Domain.InfrastructureInterfaces;

namespace YCompany.Vendor.DataAccess
{
    public class SqlStorageService : IVendorStorageService
    {
        public Task<bool> CheckHealthAsync()
        {
            return Task.FromResult(true);
        }
    }
}
