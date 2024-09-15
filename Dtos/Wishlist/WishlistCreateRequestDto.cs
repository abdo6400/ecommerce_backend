using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Wishlist
{
    public class WishlistCreateRequestDto
    {
        [Required]
        public int ProductId { get; set; }
    }
}