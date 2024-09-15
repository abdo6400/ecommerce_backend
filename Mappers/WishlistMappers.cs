using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Wishlist;
using api.Models;

namespace api.Mappers
{
    public static class WishlistMappers
    {

        public static WishlistDto ToWishlistDto(this Wishlist wishlist)
        {
            return new WishlistDto
            {
                Id = wishlist.Id,
                Product = wishlist.Product.ToProductDto()
            };
        }

        public static Wishlist ToWishlistFromCreateRequestDto(this WishlistCreateRequestDto wishlistCreateRequestDto, string userId)
        {
            return new Wishlist
            {
                UserId = userId,
                ProductId = wishlistCreateRequestDto.ProductId
            };
        }
    }
}