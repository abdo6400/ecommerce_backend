using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Wishlist
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public int ProductId { get; set; }

        // Navigation properties
        public AppUser User { get; set; } = null!;
        public Product Product { get; set; } = null!;
    }
}