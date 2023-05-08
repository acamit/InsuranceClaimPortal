using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YCompany.Payments.ExceptionHandling
{
    internal class ErrorResponse
    {
        public bool Success { get; set; }

        public string Message { get; set; }
    }
}
