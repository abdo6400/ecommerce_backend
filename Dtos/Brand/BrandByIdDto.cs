using api.Dtos.Product;

namespace api.Dtos.Brand
{
    public class BrandByIdDto
    {
        public int Id { get; set; }
        public string Image { get; set; } = string.Empty;
           public string NameEn { get; set; } = string.Empty;
           public string NameAr { get; set; } = string.Empty;
        public List<ProductDto> Products { get; set; } = [];
    }
}