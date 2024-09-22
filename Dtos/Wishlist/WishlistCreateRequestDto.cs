using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Wishlist
{
    public class WishlistCreateRequestDto
    {
        [Required]
        public int ProductId { get; set; }
    }
}