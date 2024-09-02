using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Brand;

namespace api.Dtos.SubCategory
{
    public class SubCategoryByIdDto
    {
        public int Id { get; set; }
        public string Image { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public List<BrandByIdDto> Brands { get; set; } = [];
    }
}