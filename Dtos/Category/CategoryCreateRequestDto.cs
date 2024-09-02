using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Category
{
    public class CategoryCreateRequestDto
    {
        public string Name { get; set; } = string.Empty;
        public IFormFile Image { get; set; } = null!;
    }
}