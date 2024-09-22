using api.Dtos.Coupon;

namespace api.Mappers
{
    public static class CouponMappers
    {

        public static CouponDto ToCouponDto(this Coupon coupon)
        {
            return new CouponDto
            {
                Id = coupon.Id,
                Code = coupon.Code,
                DiscountAmount = coupon.DiscountAmount,
                IsPercentage = coupon.IsPercentage,
                ExpiryDate = coupon.ExpiryDate,
                ApplicableItems = coupon.ApplicableItems
            };
        }

        public static Coupon ToCouponFromCreateRequestDto(this CouponCreateRequestDto couponDto)
        {
            return new Coupon
            {
                Code = couponDto.Code,
                DiscountAmount = couponDto.DiscountAmount,
                IsPercentage = couponDto.IsPercentage,
                ExpiryDate = couponDto.ExpiryDate,
                ApplicableItems = couponDto.ApplicableItems
            };
        }

        public static Coupon ToCouponFromUpdateRequestDto(this CouponUpdateRequestDto couponDto, int id)
        {
            return new Coupon
            {
                Id = id,
                Code = couponDto.Code,
                DiscountAmount = couponDto.DiscountAmount,
                IsPercentage = couponDto.IsPercentage,
                ExpiryDate = couponDto.ExpiryDate,
                ApplicableItems = couponDto.ApplicableItems
            };
        }
    }
}