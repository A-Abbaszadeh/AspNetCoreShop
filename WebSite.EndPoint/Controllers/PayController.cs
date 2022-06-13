using Application.Payments;
using Dto.Payment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using WebSite.EndPoint.Utilities;
using ZarinPal.Class;

namespace WebSite.EndPoint.Controllers
{
    public class PayController : Controller
    {
        private readonly Payment _payment;
        private readonly Authority _authority;
        private readonly Transactions _transactions;

        private readonly IConfiguration _configuration;
        private readonly IPaymentService _paymentService;
        private readonly string merchendId;
        public PayController(IConfiguration configuration, IPaymentService paymentService)
        {
            _configuration = configuration;
            _paymentService = paymentService;
            merchendId = _configuration["ZarinpalMerchendId"];

            var expose = new Expose();
            _payment = expose.CreatePayment();
            _authority = expose.CreateAuthority();
            _transactions = expose.CreateTransactions();
        }

        public async Task<IActionResult> Index(Guid paymentId)
        {
            var payment = _paymentService.GetPayment(paymentId);
            if (payment is null)
            {
                return NotFound();
            }

            string userId = ClaimUtility.GetUserId(User);
            if (userId != payment.UserId)
            {
                return BadRequest();
            }
            string callbackUrl = Url.Action(nameof(Verify), "pay", new { payment.Id }, protocol: Request.Scheme);
            var resultZarinpalRequest = await _payment.Request(new DtoRequest()
            {
                MerchantId = merchendId,
                Amount = payment.Amount,
                Email = payment.Email,
                Mobile = payment.PhoneNumber,
                CallbackUrl = callbackUrl,
                Description = payment.Description,
            }, Payment.Mode.sandbox);
            return Redirect($"https://sandbox.zarinpal.com/pg/StartPay/{resultZarinpalRequest.Authority}");
        }

        public IActionResult Verify()
        {
            return View();
        }
    }
}
