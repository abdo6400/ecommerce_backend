namespace api.Models
{
    public class Coupon
    {

        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public double DiscountAmount { get; set; }
        public bool IsPercentage { get; set; }
        public DateTime ExpiryDate { get; set; }

        public string ApplicableItems { get; set; } = null!;
        // Navigation properties
        public List<CouponUsage> CouponUsages { get; set; } = [];

    }
}