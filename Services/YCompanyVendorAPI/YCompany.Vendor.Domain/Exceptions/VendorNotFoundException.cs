using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YCompany.Vendor.Domain.Exceptions
{
    public class VendorNotFoundException : NotFoundException
    {
        public VendorNotFoundException(string message) : base(message)
        {
        }
    }
}
