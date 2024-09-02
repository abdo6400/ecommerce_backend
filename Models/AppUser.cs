using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace api.Models
{
    public class AppUser : IdentityUser
    {
        // Navigation property for Orders
        public List<Order> Orders { get; set; } = [];

        // Navigation property for Cart
        public List<Cart> Carts { get; set; } = [];

        // Navigation property for Review
        public List<Review> Reviews { get; set; } = [];

        // Navigation property for Wishlist
        public List<Wishlist> Wishlists { get; set; } = [];

        // Navigation property for Address
        public List<Address> Addresses { get; set; } = [];

        // Navigation property for CouponUsage
        public List<CouponUsage> CouponUsages { get; set; } = [];
    }
}