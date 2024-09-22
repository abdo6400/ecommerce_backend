namespace api.Models
{
    public class ExtraInformation
    {
        public int Id { get; set; }
        public string NameEn { get; set; } = string.Empty;
        public string NameAr { get; set; } = string.Empty;
        public string ValueEn { get; set; } = string.Empty;
        public string ValueAr { get; set; } = string.Empty;
        public int ProductId { get; set; }

        // Navigation properties
        public Product Product { get; set; } = null!;
    }
}