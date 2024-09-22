namespace api.Dtos.Category
{
    public class CategoryJsonCreateRequestDto
    {
        public string NameEn { get; set; } = string.Empty;
        public string NameAr { get; set; } = string.Empty;
        public string Image { get; set; } = null!;
    }
}