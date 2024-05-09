using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YCompany.Payments.Domain.Enitites;
using YCompany.Payments.Services.DomainServices.Interfaces;

namespace YCompanyPaymentsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("MakePayment")]
        public IActionResult MakePayment([FromBody] Payment paymentObj)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid data provided.");
                return BadRequest(ModelState);
            }
            string sessionUrl = _paymentService.CreateStripeSession(paymentObj);

            return Ok(new { sessionUrl = sessionUrl });


        }

        [HttpGet("PaymentSuccess")]
        public IActionResult PaymentSuccess()
        {
            return Ok(new { Message = "Payment successfull" });
        }
        [HttpGet("PaymentFailure")]
        public IActionResult PaymentFailure()
        {
            return BadRequest(new { Message = "Payment Cancelled" });
        }
    }
}
