using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Category;
using api.Interfaces;
using api.Mappers;
using api.Models;
using api.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace api.Controllers
{
    [ApiController]
    [Route("api/categories")]

    public class CategoryController(ICategoryRepository categoryRepository, IFileService fileService, IStringLocalizer<CategoryController> localizer) : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository = categoryRepository;
        private readonly IFileService _fileService = fileService;
        private readonly IStringLocalizer<CategoryController> _localizer = localizer;

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
                return NotFound(_localizer.GetString(AppStrings.categoryNotFound));
            }
            return Ok(category.ToCategoryByIdDto());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
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
                return BadRequest(_localizer.GetString(AppStrings.categoryAlreadyExists));
            }

            return Ok(createCategory.ToCategoryDto());
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<CategoryDto>> UpdateCategory(int id, [FromForm] CategoryUpdateRequestDto categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var imagePath = await _fileService.SaveFileAsync(categoryDto.Image, "images", "categories");
            var category = categoryDto.ToCategoryFromUpdateRequestDto(id, imagePath);
            var updatedCategory = await _categoryRepository.UpdateCategoryAsync(category);

            if (updatedCategory == null)
            {
                return NotFound(_localizer.GetString(AppStrings.categoryNotFound));
            }

            return Ok(updatedCategory.ToCategoryDto());
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<CategoryDto>> DeleteCategory(int id)
        {
            var category = await _categoryRepository.DeleteCategoryAsync(id);
            if (category == null)
            {
                return NotFound(_localizer.GetString(AppStrings.categoryNotFound));
            }

            _fileService.DeleteFile(category.Image);
            return Ok(category.ToCategoryDto());
        }

        // Bulk Create
        [HttpPost("bulk-create")]

        public async Task<ActionResult<List<CategoryDto>>> CreateCategories([FromBody] List<CategoryJsonCreateRequestDto> categoryDtos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoryDtosList = new List<CategoryDto>();

            foreach (var categoryDto in categoryDtos)
            {
                var category = categoryDto.ToCategoryFromCreateRequestJsonDto();
                var createCategory = await _categoryRepository.CreateCategoryAsync(category);

                if (createCategory == null)
                {
                    return BadRequest(_localizer.GetString(AppStrings.categoryAlreadyExists));
                }

                categoryDtosList.Add(createCategory.ToCategoryDto());
            }

            return Ok(categoryDtosList);
        }
    }
}
