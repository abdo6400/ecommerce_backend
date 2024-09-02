using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Product;
using api.Models;

namespace api.Mappers
{
    public static class ProductMappers
    {
        public static ProductDto ToProductDto(this Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Title = product.Title,
                Description = product.Description,
                Sku = product.Sku,
                Price = product.Price,
                Stock = product.Stock,
                Rating = product.Rating,
                DiscountPercentage = product.DiscountPercentage,
                Images = product.Images,
                MinimumOrderQuantity = product.MinimumOrderQuantity,
                Product_Unit = product.Product_Unit,
            };
        }

        public static ProductByIdDto ToProductByIdDto(this Product product, List<Product> relatedProducts)
        {
            return new ProductByIdDto
            {
                Id = product.Id,
                Title = product.Title,
                Description = product.Description,
                Sku = product.Sku,
                Price = product.Price,
                Stock = product.Stock,
                DiscountPercentage = product.DiscountPercentage,
                Images = product.Images,
                MinimumOrderQuantity = product.MinimumOrderQuantity,
                Brand = product.Brand.ToBrandDto(),
                Product_Unit = product.Product_Unit,
                Informations = product.Informations.Select(x => x.ToExtraInformationDto()).ToList(),
                Rating = product.Rating,
                RelatedProducts = relatedProducts.Select(x => x.ToProductDto()).ToList()
            };
        }


        public static Product ToProductFromCreateRequestDto(this ProductCreateRequestDto productDto, List<string> images)
        {
            return new Product
            {
                Title = productDto.Title,
                Description = productDto.Description,
                Price = productDto.Price,
                Sku = productDto.Sku,
                Stock = productDto.Stock,
                BrandId = productDto.BrandId,
                Images = images,
                MinimumOrderQuantity = productDto.MinimumOrderQuantity,
                DiscountPercentage = productDto.DiscountPercentage,
                Product_Unit = productDto.Product_Unit,

            };
        }


        public static Product ToProductFromUpdateRequestDto(this ProductUpdateRequestDto productDto, int id, List<string> images)
        {
            return new Product
            {
                Id = id,
                Title = productDto.Title,
                Description = productDto.Description,
                Price = productDto.Price,
                Stock = productDto.Stock,
                BrandId = productDto.BrandId,
                Images = images,
                Sku = productDto.Sku,
                MinimumOrderQuantity = productDto.MinimumOrderQuantity,
                DiscountPercentage = productDto.DiscountPercentage,
                Product_Unit = productDto.Product_Unit,
            };
        }


    }
}