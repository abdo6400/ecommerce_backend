using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Brand;
using api.Models;

namespace api.Mappers
{
    public static class BrandMappers
    {

        public static BrandByIdDto ToBrandByIdDto(this Brand brand)
        {

            return new BrandByIdDto
            {
                Id = brand.Id,
                Name = brand.Name,
                Image = brand.Image,
                Products = brand.Products.Select(x => x.ToProductDto()).ToList(),
            };

        }
        public static BrandDto ToBrandDto(this Brand brand)
        {
            return new BrandDto
            {
                Id = brand.Id,
                Name = brand.Name,
                Image = brand.Image,
            };
        }


        public static Brand ToBrandFromCreateRequestDto(this BrandCreateRequestDto brand, string image)
        {
            return new Brand
            {
                Name = brand.Name,
                Image = image,
                SubCategoryId = brand.SubCategoryId
            };
        }


        public static Brand ToBrandFromUpdateRequestDto(this BrandUpdateRequestDto brand, int id, string image)
        {
            return new Brand
            {
                Id = id,
                Name = brand.Name,
                Image = image,
                SubCategoryId = brand.SubCategoryId
            };
        }


    }
}