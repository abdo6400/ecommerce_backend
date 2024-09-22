namespace api.Models
{
    public class Delivery
    {
        public int Id { get; set; }
        public string DeliveryStatus { get; set; } = "Pending"; // E.g., Pending, Shipped, Delivered
        public DateTime? DeliveryDate { get; set; }

        public int? OrderId { get; set; }
        public Order Order { get; set; } = null!;

        // Foreign key for the DeliveryPerson
        public string? DeliveryPersonId { get; set; }
        public DeliveryPerson? DeliveryPerson { get; set; }
    }
}