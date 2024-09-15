using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Category
{
    public class CategoryCreateRequestDto
    {
        public string NameEn { get; set; } = string.Empty;
           public string NameAr { get; set; } = string.Empty;
        public IFormFile Image { get; set; } = null!;
    }
}