using api.Dtos.Brand;

namespace api.Mappers
{
    public static class BrandMappers
    {

        public static BrandByIdDto ToBrandByIdDto(this Brand brand)
        {

            return new BrandByIdDto
            {
                Id = brand.Id,
                NameAr = brand.NameAr,
                NameEn = brand.NameEn,
                Image = brand.Image,
                Products = brand.Products.Select(x => x.ToProductDto()).ToList(),
            };

        }
        public static BrandDto ToBrandDto(this Brand brand)
        {
            return new BrandDto
            {
                Id = brand.Id,
                NameAr = brand.NameAr,
                NameEn = brand.NameEn,
                Image = brand.Image,
            };
        }


        public static Brand ToBrandFromCreateRequestDto(this BrandCreateRequestDto brand, string image)
        {
            return new Brand
            {
                NameAr = brand.NameAr,
                NameEn = brand.NameEn,
                Image = image,
                SubCategoryId = brand.SubCategoryId
            };
        }


        public static Brand ToBrandFromUpdateRequestDto(this BrandUpdateRequestDto brand, int id, string image)
        {
            return new Brand
            {
                Id = id,
                NameAr = brand.NameAr,
                NameEn = brand.NameEn,
                Image = image,
                SubCategoryId = brand.SubCategoryId
            };
        }

        public static Brand ToBrandFromCreateRequestJsonDto(this BrandJsonCreateRequestDto brandJsonCreateRequestDto)
        {
            return new Brand
            {
                NameAr = brandJsonCreateRequestDto.NameAr,
                NameEn = brandJsonCreateRequestDto.NameEn,
                Image = brandJsonCreateRequestDto.Image,
                SubCategoryId = brandJsonCreateRequestDto.SubCategoryId
            };
        }


    }
}