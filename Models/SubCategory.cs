using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class SubCategory
    {

        public int Id { get; set; }
        public string Image { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        
        // Navigation property
        public Category Category { get; set; } = null!;
        public List<Brand> Brands { get; set; } = [];
    }
}