using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string OrderStatus { get; set; } = "Pending";
        public string UserId { get; set; } = null!;
        public int? CouponId { get; set; }
        public int AddressId { get; set; }

        // Navigation properties
        public Address Address { get; set; } = null!;
        public AppUser User { get; set; } = null!;
        public Coupon Coupon { get; set; } = null!;
        public List<OrderItem> OrderItems { get; set; } = [];
    }
}