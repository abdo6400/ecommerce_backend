namespace api.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public Customer User { get; set; } = null!;
        public string Comment { get; set; } = null!;
        public int Rating { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}