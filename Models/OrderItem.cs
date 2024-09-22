namespace api.Models
{
    public class OrderItem
    {
        public int Id { get; set; } 
        public int Quantity { get; set; }
        public double Price { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }

        // Navigation properties
        public Order Order { get; set; } = null!;
        public Product Product { get; set; } = null!; 
    }
}