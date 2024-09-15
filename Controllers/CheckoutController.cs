using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace api.Controllers
{
    [ApiController]
    [Route("api/checkout")]
    
    public class CheckoutController(IPaymentService paymentService) : ControllerBase
    {
        readonly IPaymentService _paymentService = paymentService;


        [HttpGet("create-payment-session")]
        [Authorize(Roles ="User")]
        public async Task<IActionResult> CreatePaymentSession()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            Dictionary<string, string> metadata = new()
            {
                { "userId", userId },
            };
            string sessionUrl = await _paymentService.CreatePaymentSession(metadata);
            return Ok(new { url = sessionUrl });
        }

        [HttpPost]
        [Route("webhook")]
        public async Task<IActionResult> HandleWebhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            Event stripeEvent;

            try
            {
                stripeEvent = EventUtility.ConstructEvent(json,
                    Request.Headers["Stripe-Signature"],
                    "whsec_bdae68432500c198e786686def30e2db8b2a8054bf0ca3ab634b6f515163b8b0");
            }
            catch (StripeException)
            {
                return BadRequest("Invalid Stripe signature.");
            }

            switch (stripeEvent.Type)
            {
                case Events.CheckoutSessionCompleted:

                    break;
                case Events.PaymentIntentSucceeded:
                    break;
                default:
                    break;
            }

            return Ok();
        }

    }
}