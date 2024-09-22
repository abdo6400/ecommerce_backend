using api.Dtos.Cart;

namespace api.Mappers
{
    public static class CartMappers
    {

        public static CartDto ToCartDto(this Cart cart)
        {
            return new CartDto
            {
                Id = cart.Id,
                Product = cart.Product.ToProductDto(),
                Quantity = cart.Quantity
            };
        }


        public static Cart ToCartFromCreateRequestDto(this CartCreateRequestDto cartDto,string userId)
        {
            return new Cart
            {
                UserId = userId,
                ProductId = cartDto.ProductID,
                Quantity = cartDto.Quantity
            };
        }


        public static Cart ToCartFromUpdateRequestDto(this CartUpdateRequestDto cartDto,int id)
        {
            return new Cart
            {
                Id = id,
                Quantity = cartDto.Quantity
            };
        }
    }
}