using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Category;
using api.Models;

namespace api.Mappers
{
    public static class CategoryMappers
    {

        public static CategoryByIdDto ToCategoryByIdDto(this Category category)
        {
            return new CategoryByIdDto
            {
                Id = category.Id,
                Name = category.Name,
                Image = category.Image,
                SubCategories = category.SubCategories.Select(x => x.ToSubCategoryByIdDto()).ToList()
            };
        }
        public static CategoryDto ToCategoryDto(this Category category)
        {
            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Image = category.Image,
            };
        }


        public static Category ToCategoryFromCreateRequestDto(this CategoryCreateRequestDto categoryCreateRequestDto, string image)
        {
            return new Category
            {
                Name = categoryCreateRequestDto.Name,
                Image = image
            };
        }

        public static Category ToCategoryFromUpdateRequestDto(this CategoryUpdateRequestDto categoryUpdateRequestDto, int id, string image)
        {

            return new Category
            {
                Id = id,
                Name = categoryUpdateRequestDto.Name,
                Image = image

            };
        }
    }
}