using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.SubCategory;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/subCategories")]
    public class SubCategoryController(ISubCategoryRepository _subCategoryRepository, IFileService _fileService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<SubCategoryDto>>> GetAll()
        {
            var subCategories = await _subCategoryRepository.GetAllAsync();
            return Ok(subCategories.Select(x => x.ToSubCategoryDto()).ToList());
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<SubCategory>> GetCategoryById(int id)
        {
            var subCategory = await _subCategoryRepository.GetSubCategoryByIdAsync(id);
            if (subCategory == null)
            {
                return NotFound("subCategory not found");
            }
            return Ok(subCategory);
        }


        [HttpPost]
        public async Task<ActionResult<SubCategoryDto>> CreateCategory([FromForm] SubCategoryCreateRequestDto subCategoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var imagePath = await _fileService.SaveFileAsync(subCategoryDto.Image, "images", "subCategories");
            var subCategory = subCategoryDto.ToSubCategoryFromCreateRequestDto(imagePath);
            var createCategory = await _subCategoryRepository.CreateSubCategoryAsync(subCategory);

            if (createCategory == null)
            {
                return BadRequest("Can not create subCategory");
            }

            return Ok(subCategory.ToSubCategoryDto());
        }



        [HttpPut("{id:int}")]
        public async Task<ActionResult<SubCategoryDto>> UpdateCategory(int id, [FromForm] SubCategoryUpdateRequestDto subCategoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var imagePath = await _fileService.SaveFileAsync(subCategoryDto.Image, "images", "subCategories");
            var subCategory = subCategoryDto.ToSubCategoryFromUpdateRequestDto(id, imagePath);
            var updateCategory = await _subCategoryRepository.UpdateSubCategoryAsync(subCategory);

            if (updateCategory == null)
            {
                return NotFound("subCategory not found");
            }
            return Ok(updateCategory.ToSubCategoryDto());
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult<SubCategoryDto>> DeleteCategory(int id)
        {
            var subCategory = await _subCategoryRepository.DeleteSubCategoryAsync(id);
            if (subCategory == null)
            {
                return NotFound("subCategory not found");
            }
            _fileService.DeleteFile(subCategory.Image);
            return Ok(subCategory.ToSubCategoryDto());
        }

    }


}