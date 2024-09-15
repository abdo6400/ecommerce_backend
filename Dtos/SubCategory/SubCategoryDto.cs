using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.SubCategory
{
    public class SubCategoryDto
    {
        public int Id { get; set; }
        public string NameEn { get; set; } = string.Empty;

        public string NameAr { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
    }
}