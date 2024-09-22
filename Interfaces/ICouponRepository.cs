namespace api.Interfaces
{
    public interface ICouponRepository
    {
        Task<Coupon?> IsCouponValidForUsageAsync(string code, string userId);

        Task<CouponUsage?> ApplyCouponAsync(string code, string userId);

        Task<List<Coupon>> GetAllAsync();
        Task<Coupon?> CreateAsync(Coupon coupon);

        Task<Coupon?> UpdateAsync(Coupon coupon);

        Task<Coupon?> DeleteAsync(int id);
    }
}