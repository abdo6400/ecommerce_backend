using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Cart;
using api.Models;

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


        public static Cart ToCartFromCreateRequestDto(this CartCreateRequestDto cartDto)
        {
            return new Cart
            {
                UserID = cartDto.UserID,
                ProductID = cartDto.ProductID,
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