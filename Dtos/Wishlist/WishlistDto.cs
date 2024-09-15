using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Product;

namespace api.Dtos.Wishlist
{
    public class WishlistDto
    {
        public int Id { get; set; }
        public ProductDto Product { get; set; } = null!;
    }
}