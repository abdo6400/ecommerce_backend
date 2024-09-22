using api.Dtos.Product;

namespace api.Dtos.Wishlist
{
    public class WishlistDto
    {
        public int Id { get; set; }
        public ProductDto Product { get; set; } = null!;
    }
}