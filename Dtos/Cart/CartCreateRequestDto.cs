using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Cart
{
    public class CartCreateRequestDto
    {
        public int ProductID { get; set; }
        public int Quantity { get; set; }

    }
}