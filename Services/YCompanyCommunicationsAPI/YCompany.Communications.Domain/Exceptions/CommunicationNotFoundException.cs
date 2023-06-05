using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YCompany.Communications.Domain.Exceptions
{
    public class CommunicationNotFoundException : NotFoundException
    {
        public CommunicationNotFoundException(string message) : base(message)
        {
        }
    }
}
