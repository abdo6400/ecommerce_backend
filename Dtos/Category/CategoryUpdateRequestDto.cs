namespace api.Dtos.Category
{
    public class CategoryUpdateRequestDto
    {
        public string NameEn { get; set; } = string.Empty;
   public string NameAr { get; set; } = string.Empty;
    public IFormFile Image { get; set; } = null!;


    }
}