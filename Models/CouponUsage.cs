using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class CouponUsage
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public int CouponId { get; set; }
        public DateTime UsedOn { get; set; }

        // Navigation properties
        public AppUser User { get; set; } = null!;
        public Coupon Coupon { get; set; } = null!;
    }
}