using api.Data;

namespace api.Repositories
{
    public class CouponRepository(ApplicationDbContext _context) : ICouponRepository
    {
        public async Task<CouponUsage?> ApplyCouponAsync(string code, string userId)
        {
            var coupon = await IsCouponValidForUsageAsync(code, userId);
            if (coupon != null)
            {
                var couponUsage = new CouponUsage
                {
                    UserId = userId,
                    CouponId = coupon.Id,
                    UsedOn = DateTime.Now
                };
                await _context.CouponUsages.AddAsync(couponUsage);
                await _context.SaveChangesAsync();
                return couponUsage;
            }
            return null;
        }

        public async Task<Coupon?> CreateAsync(Coupon coupon)
        {
            await _context.Coupons.AddAsync(coupon);
            await _context.SaveChangesAsync();

            return coupon;
        }

        public async Task<Coupon?> DeleteAsync(int id)
        {
            var couponExist = await _context.Coupons.FirstOrDefaultAsync(x => x.Id == id);
            if (couponExist == null)
            {
                return null;
            }
            _context.Coupons.Remove(couponExist);
            await _context.SaveChangesAsync();
            return couponExist;
        }

        public async Task<List<Coupon>> GetAllAsync()
        {
            return await _context.Coupons.ToListAsync();
        }

        public async Task<Coupon?> IsCouponValidForUsageAsync(string code, string userId)
        {
            var coupon = await _context.Coupons.FirstOrDefaultAsync(x => x.Code == code);
            if (coupon == null)
            {
                return null;
            }
            if (coupon.ExpiryDate < DateTime.Now)
            {
                return null;
            }
            var couponUsage = await _context.CouponUsages.FirstOrDefaultAsync(x => x.UserId == userId && x.CouponId == coupon.Id);
            if (couponUsage != null)
            {
                return null;
            }

            return coupon;
        }

        public async Task<Coupon?> UpdateAsync(Coupon coupon)
        {
            var couponExist = await _context.Coupons.FirstOrDefaultAsync(x => x.Id == coupon.Id);
            if (couponExist == null)
            {
                return null;
            }
            couponExist.Code = coupon.Code;
            couponExist.DiscountAmount = coupon.DiscountAmount;
            couponExist.ExpiryDate = coupon.ExpiryDate;
            couponExist.IsPercentage = coupon.IsPercentage;
            couponExist.ApplicableItems = coupon.ApplicableItems;
            await _context.SaveChangesAsync();
            return couponExist;
        }
    }
}