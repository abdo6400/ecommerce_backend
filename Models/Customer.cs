namespace api.Models
{
    public class Customer : BaseUser
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