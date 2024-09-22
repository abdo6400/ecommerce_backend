using api.Dtos.order;

namespace api.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController(IOrderRepository orderRepository) : ControllerBase
    {

        private readonly IOrderRepository _orderRepository = orderRepository;

        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<List<OrderDto>>> GetOrders()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var orders = await _orderRepository.GetAllAsync(userId);
            return Ok(orders.Select(x => x.ToOrderDto()).ToList());
        }
    }
}