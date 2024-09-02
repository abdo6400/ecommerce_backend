using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Cart
{
    public class CartCreateRequestDto
    {
        public String UserID { get; set; } = String.Empty;
        public int ProductID { get; set; }
        public int Quantity { get; set; }

    }
}