using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YCompany.Payments.Domain.Enitites;
using YCompany.Payments.Services.DomainServices.Interfaces;

namespace YCompany.Payments.Services.DomainServices
{
    public class PaymentService : IPaymentService
    {
        public string CreateStripeSession(Payment paymentObj)
        {
            string domain = "https://localhost:7001/api/";

            var options = new SessionCreateOptions
            {
                SuccessUrl = domain + "Payments/PaymentSuccess",
                CancelUrl = domain + "Payments/PaymentFailure",
                Mode = "payment",
                PaymentMethodTypes = new List<string> { "card" },
                CustomerEmail = paymentObj.Email,
                //BillingAddressCollection = "required", // Request billing address collection for outside india cards
                LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmount =(long) paymentObj.Amount*100,
                    Currency = "inr",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = paymentObj.PolicyName.ToString(),
                    }
                },
                Quantity = 1,
                }

            },
            };

            var service = new SessionService();
            Session session = service.Create(options);
            //Response.Headers.Add("Location", session.Url);

            return session.Url;
        }
    }
}
