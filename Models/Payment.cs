namespace api.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public string PaymentMethod { get; set; } = "Cash"; // E.g., Cash, Card, etc.
        public string PaymentStatus { get; set; } = "Pending"; // E.g., Pending, Completed, Failed
        public double AmountPaid { get; set; }

        public string? TransactionId { get; set; } // Transaction ID from the payment gateway

        // Foreign key back to the Order
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;
    }
}