namespace api.Dtos.SubCategory
{
    public class SubCategoryUpdateRequestDto
    {
        public IFormFile Image { get; set; } = null!;
        public string NameEn { get; set; } = string.Empty;

        public string NameAr { get; set; } = string.Empty;

        public int CategoryId { get; set; }
    }
}