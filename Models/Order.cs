namespace api.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public double TotalAmount { get; set; }
        public string OrderStatus { get; set; } = "Pending"; // Pending, Processing, Shipped, Delivered


        // Foreign keys
        public int? CouponId { get; set; }
        public int? AddressId { get; set; }
        public string UserId { get; set; } = null!;
        public int? PaymentId { get; set; }
        public int? DeliveryId { get; set; }
        // Navigation properties
        public Address Address { get; set; } = null!;
        public Customer User { get; set; } = null!;
        public Coupon Coupon { get; set; } = null!;
        public Payment Payment { get; set; } = null!;
        public Delivery Delivery { get; set; } = null!;
        public List<OrderItem> OrderItems { get; set; } = [];
    }
}