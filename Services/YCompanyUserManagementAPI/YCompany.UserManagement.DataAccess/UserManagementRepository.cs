using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YCompany.UserManagement.Domain.InfrastructureInterfaces.Repositories;

namespace YCompany.UserManagement.DataAccess
{
    internal sealed class UserManagementRepository : IUserManagementRepository
    {
        private readonly RepositoryDbContext _dbContext;
        public UserManagementRepository(RepositoryDbContext dbContext) => _dbContext = dbContext;
    }
}
