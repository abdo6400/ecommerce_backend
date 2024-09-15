using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using api.Dtos.Wishlist;
using api.Interfaces;
using api.Mappers;
using api.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace api.Controllers
{
    [ApiController]
    [Route("api/wishlist")]
    [Authorize(Roles = "User")]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistRepository _wishlistRepository;
        private readonly IStringLocalizer<WishlistController> _localizer;

        public WishlistController(IWishlistRepository wishlistRepository, IStringLocalizer<WishlistController> localizer)
        {
            _wishlistRepository = wishlistRepository;
            _localizer = localizer;
        }

        [HttpGet]
        public async Task<ActionResult<List<WishlistDto>>> GetWishlists()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var wishlists = await _wishlistRepository.GetAllAsync(userId);
            var wishlistDtos = wishlists.Select(x => x.ToWishlistDto()).ToList();
            return Ok(wishlistDtos);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<WishlistDto>> DeleteWishlist(int id)
        {
            var wishlist = await _wishlistRepository.DeleteAsync(id);
            if (wishlist == null)
            {
                return NotFound(_localizer[AppStrings.wishlistNotFound]);
            }
            return wishlist.ToWishlistDto();
        }

        [HttpPost]
        public async Task<ActionResult<WishlistDto>> CreateWishlist([FromBody] WishlistCreateRequestDto wishlistCreateRequestDto)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var wishlist = wishlistCreateRequestDto.ToWishlistFromCreateRequestDto(userId);
            var createdWishlist = await _wishlistRepository.CreateAsync(wishlist);
            if (createdWishlist == null)
            {
                return BadRequest(_localizer[AppStrings.failedToCreateWishlist]);
            }
            return createdWishlist.ToWishlistDto();
        }


    }
}
