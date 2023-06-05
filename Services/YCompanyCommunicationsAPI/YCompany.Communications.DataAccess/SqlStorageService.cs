using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YCompany.Communications.Domain.InfrastructureInterfaces;

namespace YCompany.Communications.DataAccess
{
    public class SqlStorageService : ICommunicationStorageService
    {
        public Task<bool> CheckHealthAsync()
        {
            return Task.FromResult(true);
        }
    }
}
