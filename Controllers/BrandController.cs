using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Brand;
using api.Interfaces;
using api.Models;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/brands")]
    public class BrandController(IBrandRepository _brandRepository, IFileService _fileService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<BrandDto>>> GetAll()
        {
            var brands = await _brandRepository.GetAllAsync();
            return Ok(brands.Select(x => x.ToBrandDto()).ToList());
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Brand>> GetBrandById(int id)
        {
            var brand = await _brandRepository.GetBrandByIdAsync(id);
            if (brand == null)
            {
                return NotFound("Brand not found");
            }
            return Ok(brand);
        }
        [HttpPost]
        public async Task<ActionResult<BrandDto>> CreateBrand([FromForm] BrandCreateRequestDto brandDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var imagePath = await _fileService.SaveFileAsync(brandDto.Image, "images", "Brand");
            var brand = brandDto.ToBrandFromCreateRequestDto(imagePath);
            var createBrand = await _brandRepository.CreateBrandAsync(brand);

            if (createBrand == null)
            {
                return BadRequest("Can not create Brand");
            }

            return Ok(brand.ToBrandDto());
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<BrandDto>> UpdateBrand(int id, [FromForm] BrandUpdateRequestDto brandDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var imagePath = await _fileService.SaveFileAsync(brandDto.Image, "images", "Brand");
            var brand = brandDto.ToBrandFromUpdateRequestDto(id, imagePath);
            await _brandRepository.UpdateBrandAsync(brand);

            if (brand == null)
            {
                return NotFound("Brand not found");
            }
            return Ok(brand.ToBrandDto());
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<BrandDto>> DeleteBrand(int id)
        {
            var brand = await _brandRepository.DeleteBrandAsync(id);
            if (brand == null)
            {
                return NotFound("Brand not found");
            }
            _fileService.DeleteFile(brand.Image);
            return Ok(brand.ToBrandDto());
        }
    }
}