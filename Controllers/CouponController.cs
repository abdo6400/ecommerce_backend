using api.Dtos.Coupon;

namespace api.Controllers
{
    [ApiController]
    [Route("api/coupons")]
    public class CouponController(ICouponRepository couponRepository, IStringLocalizer<CouponController> localizer) : ControllerBase
    {
        readonly ICouponRepository _couponRepository = couponRepository;
        readonly IStringLocalizer<CouponController> _localizer = localizer;

        [HttpPost]
        [Route("check-coupon")]
        [Authorize(Roles = "User")]
        [ApiExplorerSettings(GroupName = "v1-customer")]
        public async Task<ActionResult<CouponDto>> CheckCoupon([FromBody] CheckCouponRequestDto checkCouponRequestDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var coupon = await _couponRepository.IsCouponValidForUsageAsync(checkCouponRequestDto.Code, userId);
            if (coupon == null)
            {
                return BadRequest(_localizer.GetString(AppStrings.couponNotValid).Value);
            }
            return Ok(coupon.ToCouponDto());
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ApiExplorerSettings(GroupName = "v1-admin")]
        public async Task<ActionResult<List<CouponDto>>> GetAll()
        {
            var coupons = await _couponRepository.GetAllAsync();
            return Ok(coupons.Select(x => x.ToCouponDto()).ToList());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ApiExplorerSettings(GroupName = "v1-admin")]
        public async Task<ActionResult<CouponDto>> Create(CouponCreateRequestDto couponDto)
        {
            var coupon = couponDto.ToCouponFromCreateRequestDto();
            var createdCoupon = await _couponRepository.CreateAsync(coupon);
            if (createdCoupon == null)
            {
                return BadRequest(_localizer.GetString(AppStrings.couponNotCreated).Value);
            }
            return Ok(createdCoupon.ToCouponDto());
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [ApiExplorerSettings(GroupName = "v1-admin")]
        public async Task<ActionResult<CouponDto>> Delete(int id)
        {
            var coupon = await _couponRepository.DeleteAsync(id);
            if (coupon == null)
            {
                return NotFound(_localizer.GetString(AppStrings.idNotMatch).Value);
            }
            return Ok(coupon.ToCouponDto());
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [ApiExplorerSettings(GroupName = "v1-admin")]
        public async Task<ActionResult<CouponDto>> Update(int id, CouponUpdateRequestDto couponDto)
        {
            var coupon = couponDto.ToCouponFromUpdateRequestDto(id);
            var updatedCoupon = await _couponRepository.UpdateAsync(coupon);
            if (updatedCoupon == null)
            {
                return NotFound(_localizer.GetString(AppStrings.idNotMatch).Value);
            }
            return Ok(updatedCoupon.ToCouponDto());
        }
    }
}