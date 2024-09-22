namespace api.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public String UserId { get; set; } = null!;
        //Navigation Properties
        public Customer User { get; set; } = null!;
        public Product Product { get; set; } = null!;
    }
}