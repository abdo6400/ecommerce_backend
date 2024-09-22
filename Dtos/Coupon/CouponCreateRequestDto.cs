namespace api.Dtos.Coupon
{
    public class CouponCreateRequestDto
    {
        public string Code { get; set; } = null!;
        public double DiscountAmount { get; set; }
        public bool IsPercentage { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string ApplicableItems { get; set; } = null!;
    }
}