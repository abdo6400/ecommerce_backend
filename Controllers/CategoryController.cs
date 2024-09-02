using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Category;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController(ICategoryRepository _categoryRepository, IFileService _fileService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<CategoryDto>>> GetAll()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return Ok(categories.Select(x => x.ToCategoryDto()).ToList());
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<CategoryByIdDto>> GetCategoryById(int id)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound("Category not found");
            }
            return Ok(category.ToCategoryByIdDto());
        }


        [HttpPost]
        public async Task<ActionResult<CategoryDto>> CreateCategory([FromForm] CategoryCreateRequestDto categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var imagePath = await _fileService.SaveFileAsync(categoryDto.Image, "images", "categories");
            var category = categoryDto.ToCategoryFromCreateRequestDto(imagePath);
            var createCategory = await _categoryRepository.CreateCategoryAsync(category);

            if (createCategory == null)
            {
                return BadRequest("Can not create category");
            }

            return Ok(category.ToCategoryDto());
        }



        [HttpPut("{id:int}")]
        public async Task<ActionResult<CategoryDto>> UpdateCategory(int id, [FromForm] CategoryUpdateRequestDto categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var imagePath = await _fileService.SaveFileAsync(categoryDto.Image, "images", "categories");
            var category = categoryDto.ToCategoryFromUpdateRequestDto(id, imagePath);
            await _categoryRepository.UpdateCategoryAsync(category);

            if (category == null)
            {
                return NotFound("Category not found");
            }
            return Ok(category.ToCategoryDto());
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CategoryDto>> DeleteCategory(int id)
        {
            var category = await _categoryRepository.DeleteCategoryAsync(id);
            if (category == null)
            {
                return NotFound("Category not found");
            }
            _fileService.DeleteFile(category.Image);
            return Ok(category.ToCategoryDto());
        }

    }
}