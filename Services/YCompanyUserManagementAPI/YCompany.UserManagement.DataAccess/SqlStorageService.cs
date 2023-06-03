using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YCompany.UserManagement.Domain.InfrastructureInterfaces;

namespace YCompany.UserManagement.DataAccess
{
    public class SqlStorageService : IUserManagementStorageService
    {
        public Task<bool> CheckHealthAsync()
        {
            return Task.FromResult(true);
        }
    }
}
