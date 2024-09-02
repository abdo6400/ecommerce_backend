using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Product;
using api.Dtos.SubCategory;

namespace api.Dtos.Brand
{
    public class BrandByIdDto
    {
        public int Id { get; set; }
        public string Image { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public List<ProductDto> Products { get; set; } = [];
    }
}