using api.Dtos.Wishlist;

namespace api.Mappers
{
    public static class WishlistMappers
    {

        public static WishlistDto ToWishlistDto(this Wishlist wishlist)
        {
            return new WishlistDto
            {
                Id = wishlist.Id,
                Product = wishlist.Product.ToProductDto()
            };
        }

        public static Wishlist ToWishlistFromCreateRequestDto(this WishlistCreateRequestDto wishlistCreateRequestDto, string userId)
        {
            return new Wishlist
            {
                UserId = userId,
                ProductId = wishlistCreateRequestDto.ProductId
            };
        }
    }
}