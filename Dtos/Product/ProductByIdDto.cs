using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Brand;
using api.Dtos.ExtraInformation;
using api.Models;

namespace api.Dtos.Product
{
    public class ProductByIdDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Sku { get; set; } = string.Empty;
        public double Price { get; set; }
        public int Stock { get; set; }
        public int MinimumOrderQuantity { get; set; }
        public double DiscountPercentage { get; set; }
        public double Rating { get; set; }
        public string Product_Unit { get; set; } = string.Empty;
        public List<string> Images { get; set; } = [];
        public BrandDto Brand { get; set; } = null!;
        public List<ExtraInformationDto> Informations { get; set; } = [];

        public List<ProductDto> RelatedProducts { get; set; } = [];
    }
}