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

        public static SubCategoryByIdDto ToSubCategoryByIdDto(this SubCategory subCategory)
        {
            return new SubCategoryByIdDto
            {
                Id = subCategory.Id,
                NameEn = subCategory.NameEn,
                NameAr = subCategory.NameAr,
                Image = subCategory.Image,
                Brands = subCategory.Brands.Select(x => x.ToBrandByIdDto()).ToList()
            };
        }
        public static SubCategoryDto ToSubCategoryDto(this SubCategory subCategory)
        {
            return new SubCategoryDto
            {
                Id = subCategory.Id,
                NameEn = subCategory.NameEn,
                NameAr = subCategory.NameAr,
                Image = subCategory.Image
            };
        }

        public static SubCategory ToSubCategoryFromCreateRequestDto(this SubCategoryCreateRequestDto subCategoryCreateRequestDto, string image)
        {
            return new SubCategory
            {
                NameEn = subCategoryCreateRequestDto.NameEn,
                NameAr = subCategoryCreateRequestDto.NameAr,
                Image = image,
                CategoryId = subCategoryCreateRequestDto.CategoryId
            };
        }


        public static SubCategory ToSubCategoryFromUpdateRequestDto(this SubCategoryUpdateRequestDto subCategoryUpdateRequestDto, int id, string image)
        {
            return new SubCategory
            {
                Id = id,
                NameEn = subCategoryUpdateRequestDto.NameEn,
                NameAr = subCategoryUpdateRequestDto.NameAr,
                Image = image,
                CategoryId = subCategoryUpdateRequestDto.CategoryId
            };
        }

        public static SubCategory ToSubCategoryFromCreateRequestJsonDto(this SubCategoryJsonCreateRequestDto subCategoryJsonCreateRequestDto)
        {
            return new SubCategory
            {

                NameEn = subCategoryJsonCreateRequestDto.NameEn,
                NameAr = subCategoryJsonCreateRequestDto.NameAr,
                Image = subCategoryJsonCreateRequestDto.Image,
                CategoryId = subCategoryJsonCreateRequestDto.CategoryId
            };
        }

    }
}