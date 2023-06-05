using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YCompany.Reporting.Domain.Exceptions
{
    internal class ReportNotFoundException : NotFoundException
    {
        public ReportNotFoundException(string message) : base(message)
        {
        }
    }
}
