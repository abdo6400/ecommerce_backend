using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Coupon
    {

        public int Id { get; set; }
        public string CouponCode { get; set; } = string.Empty;
        public string DiscountType { get; set; } = string.Empty; // "Percentage" or "FixedAmount"
        public double DiscountValue { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int UsageLimit { get; set; }
        public int TimesUsed { get; set; }
        public bool IsActive { get; set; }

        // Navigation properties
        public List<CouponUsage> CouponUsages { get; set; } = [];
    }
}