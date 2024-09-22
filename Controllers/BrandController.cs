using api.Dtos.Brand;

namespace api.Controllers
{
    [ApiController]
    [Route("api/brands")]
    
    public class BrandController(IBrandRepository brandRepository, IFileService fileService, IStringLocalizer<BrandController> localizer) : ControllerBase
    {
        private readonly IBrandRepository _brandRepository = brandRepository;
        private readonly IFileService _fileService = fileService;
        private readonly IStringLocalizer<BrandController> _localizer = localizer;

        [HttpGet]
        [ApiExplorerSettings(GroupName = "v1-customer")]
        public async Task<ActionResult<List<BrandDto>>> GetAll()
        {
            var brands = await _brandRepository.GetAllAsync();
            return Ok(brands.Select(x => x.ToBrandDto()).ToList());
        }

        [HttpGet("{id:int}")]
        [ApiExplorerSettings(GroupName = "v1-customer")]
        public async Task<ActionResult<BrandDto>> GetBrandById(int id)
        {
            var brand = await _brandRepository.GetBrandByIdAsync(id);
            if (brand == null)
            {
                return NotFound(_localizer.GetString(AppStrings.brandNotFound).Value);
            }
            return Ok(brand.ToBrandDto());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ApiExplorerSettings(GroupName = "v1-admin")]
        public async Task<ActionResult<BrandDto>> CreateBrand([FromForm] BrandCreateRequestDto brandDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var imagePath = await _fileService.SaveFileAsync(brandDto.Image, "images", "Brand");
            var brand = brandDto.ToBrandFromCreateRequestDto(imagePath);
            var createdBrand = await _brandRepository.CreateBrandAsync(brand);

            if (createdBrand == null)
            {
                return BadRequest(_localizer.GetString(AppStrings.brandCannotBeCreated).Value);
            }

            return Ok(createdBrand.ToBrandDto());

        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        [ApiExplorerSettings(GroupName = "v1-admin")]
        public async Task<ActionResult<BrandDto>> UpdateBrand(int id, [FromForm] BrandUpdateRequestDto brandDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var imagePath = await _fileService.SaveFileAsync(brandDto.Image, "images", "Brand");
            var brand = brandDto.ToBrandFromUpdateRequestDto(id, imagePath);
            var updatedBrand = await _brandRepository.UpdateBrandAsync(brand);

            if (updatedBrand == null)
            {
                return NotFound(_localizer.GetString(AppStrings.brandNotFound).Value);
            }

            return Ok(updatedBrand.ToBrandDto());


        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        [ApiExplorerSettings(GroupName = "v1-admin")]
        public async Task<ActionResult<BrandDto>> DeleteBrand(int id)
        {

            var brand = await _brandRepository.DeleteBrandAsync(id);
            if (brand == null)
            {
                return NotFound(_localizer.GetString(AppStrings.brandNotFound).Value);
            }

            _fileService.DeleteFile(brand.Image);
            return Ok(brand.ToBrandDto());

        }

        [HttpPost("bulk-create-brands")]
        [ApiExplorerSettings(GroupName = "v1-admin")]
        
        public async Task<ActionResult<List<BrandDto>>> CreateBrands([FromBody] List<BrandJsonCreateRequestDto> brandDtos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var brandDtosList = new List<BrandDto>();

            foreach (var brandDto in brandDtos)
            {
                var brand = brandDto.ToBrandFromCreateRequestJsonDto();
                var createdBrand = await _brandRepository.CreateBrandAsync(brand);

                if (createdBrand == null)
                {
                    return BadRequest(_localizer.GetString(AppStrings.brandCannotBeCreated).Value);
                }

                brandDtosList.Add(createdBrand.ToBrandDto());
            }

            return Ok(brandDtosList);
        }
    }
}
