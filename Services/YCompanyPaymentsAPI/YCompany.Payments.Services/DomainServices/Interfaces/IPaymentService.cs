using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YCompany.Payments.Domain.Enitites;

namespace YCompany.Payments.Services.DomainServices.Interfaces
{
    public interface IPaymentService
    {
        string CreateStripeSession(Payment paymentObj);
    }
}
