using api.Dtos.Category;

namespace api.Mappers
{
    public static class CategoryMappers
    {

        public static CategoryByIdDto ToCategoryByIdDto(this Category category)
        {
            return new CategoryByIdDto
            {
                Id = category.Id,
                NameEn = category.NameEn,
                NameAr = category.NameAr,
                Image = category.Image,
                SubCategories = category.SubCategories.Select(x => x.ToSubCategoryByIdDto()).ToList()
            };
        }
        public static CategoryDto ToCategoryDto(this Category category)
        {
            return new CategoryDto
            {
                Id = category.Id,
                NameEn = category.NameEn,
                NameAr = category.NameAr,
                Image = category.Image,
            };
        }


        public static Category ToCategoryFromCreateRequestDto(this CategoryCreateRequestDto categoryCreateRequestDto, string image)
        {
            return new Category
            {
                NameEn = categoryCreateRequestDto.NameEn,
                NameAr = categoryCreateRequestDto.NameAr,
                Image = image
            };
        }

        public static Category ToCategoryFromUpdateRequestDto(this CategoryUpdateRequestDto categoryUpdateRequestDto, int id, string image)
        {

            return new Category
            {
                Id = id,
                NameEn = categoryUpdateRequestDto.NameEn,
                NameAr = categoryUpdateRequestDto.NameAr,
                Image = image

            };
        }
        public static Category ToCategoryFromCreateRequestJsonDto(this CategoryJsonCreateRequestDto categoryJsonCreateRequestDto)
        {
            return new Category
            {
                NameEn = categoryJsonCreateRequestDto.NameEn,
                NameAr = categoryJsonCreateRequestDto.NameAr,
                Image = categoryJsonCreateRequestDto.Image

            };
        }

    }
}