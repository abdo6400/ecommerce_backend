using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.SubCategory;
using api.Models;

namespace api.Mappers
{
    public static class SubCategoryMappers
    {

        public static SubCategoryByIdDto ToSubCategoryByIdDto(this SubCategory subCategory){
            return new SubCategoryByIdDto
            {
                Id = subCategory.Id,
                Name = subCategory.Name,
                Image = subCategory.Image,
                Brands = subCategory.Brands.Select(x => x.ToBrandByIdDto()).ToList()
            };
        }
        public static SubCategoryDto ToSubCategoryDto(this SubCategory subCategory)
        {
            return new SubCategoryDto
            {
                Id = subCategory.Id,
                Name = subCategory.Name,
                Image = subCategory.Image
            };
        }

        public static SubCategory ToSubCategoryFromCreateRequestDto(this SubCategoryCreateRequestDto subCategoryCreateRequestDto, string image)
        {
            return new SubCategory
            {
                Name = subCategoryCreateRequestDto.Name,
                Image = image,
                CategoryId = subCategoryCreateRequestDto.CategoryId
            };
        }


        public static SubCategory ToSubCategoryFromUpdateRequestDto(this SubCategoryUpdateRequestDto subCategoryUpdateRequestDto, int id, string image)
        {
            return new SubCategory
            {
                Id = id,
                Name = subCategoryUpdateRequestDto.Name,
                Image = image,
                CategoryId = subCategoryUpdateRequestDto.CategoryId
            };
        }

    }
}