using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace api.Models
{
    public class Product
    {

        public int Id { get; set; }

        public string TitleEn { get; set; } = string.Empty;
        public string DescriptionEn { get; set; } = string.Empty;
        public string TitleAr { get; set; } = string.Empty;
        public string DescriptionAr { get; set; } = string.Empty;

        public string Sku { get; set; } = string.Empty;
        public double Price { get; set; }
        public int Stock { get; set; }
        public int MinimumOrderQuantity { get; set; }
        public double DiscountPercentage { get; set; }
        public string Product_Unit { get; set; } = string.Empty;
        [NotMapped]
        public double Rating
        {
            get
            {
                return Reviews.Count != 0 ? Reviews.Average(r => r.Rating) : 0.0;
            }
        }
        public int BrandId { get; set; }
        public List<string> Images { get; set; } = [];

        // Navigation propert 
        public Brand Brand { get; set; } = null!;
        public List<Review> Reviews { get; set; } = [];
        public List<ExtraInformation> Informations { get; set; } = [];
        public List<Wishlist> Wishlists { get; set; } = [];

        public List<Cart> Carts { get; set; } = [];

        public List<OrderItem> OrderItems { get; set; } = [];
    }
}