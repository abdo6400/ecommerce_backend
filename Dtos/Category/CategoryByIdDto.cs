using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.SubCategory;

namespace api.Dtos.Category
{
    public class CategoryByIdDto
    {
        public int Id { get; set; }
        public string Image { get; set; } = string.Empty;
        public string NameEn { get; set; } = string.Empty;
        public string NameAr { get; set; } = string.Empty;
        public List<SubCategoryByIdDto> SubCategories { get; set; } = [];
    }
}