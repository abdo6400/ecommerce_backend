using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.ExtraInformation;
using api.Models;

namespace api.Dtos.Product
{
    public class ProductUpdateRequestDto
    {

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Sku { get; set; } = string.Empty;
        public double Price { get; set; }
        public int Stock { get; set; }
        public int MinimumOrderQuantity { get; set; }
        public double DiscountPercentage { get; set; }
        public int BrandId { get; set; }
        public List<IFormFile> Images { get; set; } = [];
        public string Product_Unit { get; set; } = string.Empty;
        public List<ExtraInformationDto> ExtraInformations { get; set; } = [];
    }
}