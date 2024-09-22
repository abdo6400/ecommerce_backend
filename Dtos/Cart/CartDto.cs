using api.Dtos.Product;

namespace api.Dtos.Cart
{
    public class CartDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        //Navigation Properties
        public ProductDto Product { get; set; } = null!;
    }
}