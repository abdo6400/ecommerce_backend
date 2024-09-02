using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public String UserID { get; set; } = null!;
        //Navigation Properties
        public AppUser User { get; set; } = null!;
        public Product Product { get; set; } = null!;
    }
}