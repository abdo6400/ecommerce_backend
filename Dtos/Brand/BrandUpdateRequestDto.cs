namespace api.Dtos.Brand
{
    public class BrandUpdateRequestDto
    {

        public IFormFile Image { get; set; } = null!;
        public string NameEn { get; set; } = string.Empty;
           public string NameAr { get; set; } = string.Empty;
        public int SubCategoryId { get; set; }
    }
}