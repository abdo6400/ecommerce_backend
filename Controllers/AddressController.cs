using api.Dtos.Address;

namespace api.Controllers
{
    [ApiController]
    [Route("api/addresses")]
    [Authorize(Roles = "User")]
    [ApiExplorerSettings(GroupName = "v1-customer")]
    public class AddressController(IAddressRepository addressRepository, IStringLocalizer<AddressController> localizer) : ControllerBase
    {
        readonly IAddressRepository _addressRepository = addressRepository;
        readonly IStringLocalizer<AddressController> _localizer = localizer;

        [HttpGet]
        public async Task<ActionResult<List<AddressDto>>> GetAddresses()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var addresses = await _addressRepository.GetAllAsync(userId);
            return Ok(addresses.Select(x => x.ToAddressDto()).ToList());
        }

        [HttpPost]
        public async Task<ActionResult<AddressDto>> CreateAddress(AddressCreateRequestDto addressDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var address = addressDto.ToAddressCreateRequestDto(userId);
            var createAddress = await _addressRepository.CreateAsync(address);
            if (createAddress == null)
            {
                return BadRequest(_localizer.GetString(AppStrings.addressNotCreated).Value);
            }
            return Ok(createAddress.ToAddressDto());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<AddressDto>> DeleteAddress(int id)
        {
            var address = await _addressRepository.DeleteAsync(id);
            if (address == null)
            {
                return BadRequest(_localizer.GetString(AppStrings.idNotMatch).Value);
            }
            return Ok(address.ToAddressDto());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AddressDto>> UpdateAddress(int id, AddressUpdateRequestDto addressDto)
        {
            var address = addressDto.ToAddressUpdateRequestDto(id);
            var updatedAddress = await _addressRepository.UpdateAsync(address);
            if (updatedAddress == null)
            {
                return BadRequest(_localizer.GetString(AppStrings.addressNotUpdated).Value);
            }
            return Ok(updatedAddress.ToAddressDto());
        }
    }
}