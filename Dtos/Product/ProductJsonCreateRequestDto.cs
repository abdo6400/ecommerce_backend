using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.ExtraInformation;

namespace api.Dtos.Product
{
    public class ProductJsonCreateRequestDto
    {
        public string TitleEn { get; set; } = string.Empty;
        public string DescriptionEn { get; set; } = string.Empty;

        public string TitleAr { get; set; } = string.Empty;
        public string DescriptionAr { get; set; } = string.Empty;
        public string Sku { get; set; } = string.Empty;
        public double Price { get; set; }
        public int Stock { get; set; }
        public int MinimumOrderQuantity { get; set; }
        public double DiscountPercentage { get; set; }
        public int BrandId { get; set; }
        public List<string> Images { get; set; } = [];

        public List<ExtraInformationDto> ExtraInformations { get; set; } = [];
        public string Product_Unit { get; set; } = string.Empty;
    }
}