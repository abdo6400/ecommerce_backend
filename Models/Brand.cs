namespace api.Models
{
    public class Brand
    {

        public int Id { get; set; }
        public string Image { get; set; } = string.Empty;
        public string NameEn { get; set; } = string.Empty;

        public string NameAr { get; set; } = string.Empty;
        public int SubCategoryId { get; set; }
        // Navigation property to link products
        public SubCategory SubCategory { get; set; } = null!;
        public List<Product> Products { get; set; } = [];
    }
}