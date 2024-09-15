using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Brand;
using api.Interfaces;
using api.Models;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using api.Resources;
using Microsoft.AspNetCore.Authorization;

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
        public async Task<ActionResult<List<BrandDto>>> GetAll()
        {
            var brands = await _brandRepository.GetAllAsync();
            return Ok(brands.Select(x => x.ToBrandDto()).ToList());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<BrandDto>> GetBrandById(int id)
        {
            var brand = await _brandRepository.GetBrandByIdAsync(id);
            if (brand == null)
            {
                return NotFound(_localizer.GetString(AppStrings.brandNotFound));
            }
            return Ok(brand.ToBrandDto());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
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
                return BadRequest(_localizer.GetString(AppStrings.brandCannotBeCreated));
            }

            return Ok(createdBrand.ToBrandDto());

        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
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
                return NotFound(_localizer.GetString(AppStrings.brandNotFound));
            }

            return Ok(updatedBrand.ToBrandDto());


        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<BrandDto>> DeleteBrand(int id)
        {

            var brand = await _brandRepository.DeleteBrandAsync(id);
            if (brand == null)
            {
                return NotFound(_localizer.GetString(AppStrings.brandNotFound));
            }

            _fileService.DeleteFile(brand.Image);
            return Ok(brand.ToBrandDto());

        }

        [HttpPost("bulk-create-brands")]
        
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
                    return BadRequest(_localizer.GetString(AppStrings.brandCannotBeCreated));
                }

                brandDtosList.Add(createdBrand.ToBrandDto());
            }

            return Ok(brandDtosList);
        }
    }
}
