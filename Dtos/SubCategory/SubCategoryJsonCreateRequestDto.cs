namespace api.Dtos.SubCategory
{
    public class SubCategoryJsonCreateRequestDto
    {
                public string Image { get; set; } = null!;
        public string NameEn { get; set; } = string.Empty;

        public string NameAr { get; set; } = string.Empty;


        public int CategoryId { get; set; }
    }
}