using api.Dtos.Checkout;
using api.Dtos.order;
using api.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Stripe;
using Stripe.Checkout;

namespace api.Controllers
{
    [ApiController]
    [Route("api/checkout")]
    [ApiExplorerSettings(GroupName = "v1-customer")]
    public class CheckoutController(IPaymentService paymentService, IOrderRepository orderRepository, ICouponRepository couponRepository, IOptions<StripeSettings> stripeSettings) : ControllerBase
    {
        readonly IPaymentService _paymentService = paymentService;
        readonly IOrderRepository _orderRepository = orderRepository;

        readonly ICouponRepository _couponRepository = couponRepository;

        readonly StripeSettings _stripeSettings = stripeSettings.Value;

        [HttpPost("card")]
        [Authorize(Roles = "User")]

        public async Task<IActionResult> CheckoutCard([FromBody] CheckoutCardRequestDto checkoutCardRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return await CreatePaymentSession(checkoutCardRequest);
        }

        [HttpPost("cash")]
        [Authorize(Roles = "User")]

        public async Task<ActionResult<OrderDto>> CheckoutCash([FromBody] CheckoutCashRequestDto checkoutCardRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            if (checkoutCardRequest.Code != null)
            {
                var coupon = await _couponRepository.IsCouponValidForUsageAsync(checkoutCardRequest.Code, userId);
                if (coupon == null)
                {
                    return BadRequest("Coupon not valid");
                }
            }
            var order = await _orderRepository.CreateAsync(userId, checkoutCardRequest.AddressId, checkoutCardRequest.Code, "cash");
            if (order == null)
            {
                return BadRequest("Order not created");
            }
            return Ok(order.ToOrderDto());
        }

        private async Task<IActionResult> CreatePaymentSession(CheckoutCardRequestDto checkoutCardRequest)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            if (checkoutCardRequest.Code != null)
            {
                var coupon = await _couponRepository.IsCouponValidForUsageAsync(checkoutCardRequest.Code, userId);
                if (coupon == null)
                {
                    return BadRequest("Coupon not valid");
                }
            }
            Dictionary<string, string> metadata = new()
            {
                { "userId", userId },
                { "addressId", checkoutCardRequest.AddressId.ToString() },
                { "code", checkoutCardRequest.Code ?? "" }
            };


            string sessionUrl = await _paymentService.CreatePaymentSession(metadata);
            return Ok(new { url = sessionUrl });
        }

        [HttpPost]
        [Route("webhook")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> HandleWebhook()
        {
            try
            {
                var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
                Event stripeEvent = EventUtility.ConstructEvent(json,
                            Request.Headers["Stripe-Signature"],
                            _stripeSettings.StripeSignature);

                if (stripeEvent.Type == Events.CheckoutSessionCompleted || stripeEvent.Type == Events.CheckoutSessionAsyncPaymentSucceeded)
                {
                    Session session = (stripeEvent.Data.Object as Session)!;
                    await _orderRepository.CreateAsync(session.Metadata["userId"], int.Parse(session.Metadata["addressId"]), !session.Metadata["code"].IsNullOrEmpty() ? session.Metadata["code"] : null, "card", session.Id);
                }

                return Ok();
            }
            catch (StripeException)
            {
                return BadRequest("Invalid Stripe signature.");
            }
        }

    }
}