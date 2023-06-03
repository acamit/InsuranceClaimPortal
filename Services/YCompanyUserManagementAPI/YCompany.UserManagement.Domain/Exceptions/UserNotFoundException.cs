using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YCompany.UserManagement.Domain.Exceptions
{
    public abstract class UserNotFoundException : NotFoundException
    {
        protected UserNotFoundException(string message) : base(message)
        {
        }
    }
}
