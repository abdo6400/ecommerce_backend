namespace api.Dtos.Brand
{
    public class BrandJsonCreateRequestDto
    {
        public string Image { get; set; } = null!;
        public string NameEn { get; set; } = string.Empty;
           public string NameAr { get; set; } = string.Empty;
        public int SubCategoryId { get; set; }
    }
}