using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Cart;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/carts")]
    public class CartController(ICartRepository _cartRepository) : ControllerBase
    {


        [HttpGet]
        public async Task<ActionResult<List<CartDto>>> GetAll()
        {
            var carts = await _cartRepository.GetAllAsync();
            return Ok(carts.Select(x => x.ToCartDto()).ToList());
        }


        [HttpPost]
        public async Task<ActionResult<CartDto>> CreateCart(CartCreateRequestDto cartDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var cart = cartDto.ToCartFromCreateRequestDto();
            var createCart = await _cartRepository.CreateCartAsync(cart);
            if (createCart == null)
            {
                return BadRequest("Can not create cart");
            }
            return Ok("Cart created successfully");
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<CartDto>> UpdateCart(int id, CartUpdateRequestDto cartDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var cart = cartDto.ToCartFromUpdateRequestDto(id);
            var updateCart = await _cartRepository.UpdateCartAsync(cart);
            if (updateCart == null)
            {
                return NotFound("Cart not found");
            }
            return Ok(cart.ToCartDto());
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<CartDto>> DeleteCart(int id)
        {
            var cart = await _cartRepository.DeleteCartAsync(id);
            if (cart == null)
            {
                return NotFound("Cart not found");
            }
            return Ok(cart.ToCartDto());
        }
    }
}