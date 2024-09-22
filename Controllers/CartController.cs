using api.Dtos.Cart;

namespace api.Controllers
{
    [ApiController]
    [Route("api/carts")]
    [Authorize(Roles = "User")]
    [ApiExplorerSettings(GroupName = "v1-customer")]
    public class CartController(ICartRepository cartRepository, IStringLocalizer<CartController> localizer) : ControllerBase
    {
        private readonly ICartRepository _cartRepository = cartRepository;
        private readonly IStringLocalizer<CartController> _localizer = localizer;

        [HttpGet]
        public async Task<ActionResult<List<CartDto>>> GetAll()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var carts = await _cartRepository.GetAllAsync(userId);
            return Ok(carts.Select(x => x.ToCartDto()).ToList());
        }

        [HttpPost]
        public async Task<ActionResult<CartDto>> CreateCart(CartCreateRequestDto cartDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var cart = cartDto.ToCartFromCreateRequestDto(userId);
            var createdCart = await _cartRepository.CreateCartAsync(cart);

            if (createdCart == null)
            {
                return BadRequest(_localizer.GetString(AppStrings.cartNotCreated).Value);
            }

            return Ok(createdCart.ToCartDto());
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<CartDto>> UpdateCart(int id, CartUpdateRequestDto cartDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cart = cartDto.ToCartFromUpdateRequestDto(id);
            var updatedCart = await _cartRepository.UpdateCartAsync(cart);

            if (updatedCart == null)
            {
                return NotFound(_localizer.GetString(AppStrings.cartNotFound).Value);
            }

            return Ok(updatedCart.ToCartDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CartDto>> DeleteCart(int id)
        {
            var cart = await _cartRepository.DeleteCartAsync(id);

            if (cart == null)
            {
                return NotFound(_localizer.GetString(AppStrings.cartNotFound).Value);
            }

            return Ok(cart.ToCartDto());
        }
    }
}
