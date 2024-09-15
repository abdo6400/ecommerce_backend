using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Category;
using api.Dtos.ExtraInformation;
using api.Models;

namespace api.Dtos.Product
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string TitleEn { get; set; } = string.Empty;
        public string DescriptionEn { get; set; } = string.Empty;


        public string TitleAr { get; set; } = string.Empty;
        public string DescriptionAr { get; set; } = string.Empty;


        public string Sku { get; set; } = string.Empty;
        public string Product_Unit { get; set; } = string.Empty;
        public double Price { get; set; }
        public int Stock { get; set; }
        public int MinimumOrderQuantity { get; set; }
        public double DiscountPercentage { get; set; }
        public double Rating { get; set; }
        public List<string> Images { get; set; } = [];
    }
}