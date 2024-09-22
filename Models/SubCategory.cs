namespace api.Models
{
    public class SubCategory
    {

        public int Id { get; set; }
        public string Image { get; set; } = string.Empty;
        public string NameEn { get; set; } = string.Empty;
        public string NameAr { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        
        // Navigation property
        public Category Category { get; set; } = null!;
        public List<Brand> Brands { get; set; } = [];
    }
}