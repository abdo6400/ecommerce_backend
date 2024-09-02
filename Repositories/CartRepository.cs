using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class CartRepository(ApplicationDbContext _context) : ICartRepository
    {
        public async Task<Cart?> CreateCartAsync(Cart cart)
        {
            await _context.Carts.AddAsync(cart);
            await _context.SaveChangesAsync();
            return cart;
        }

        public async Task<Cart?> DeleteCartAsync(int id)
        {
            var cart = await _context.Carts.Include(x => x.Product).FirstOrDefaultAsync(x => x.Id == id);
            if (cart == null)
            {
                return null;
            }
            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();
            return cart;
        }

        public async Task<List<Cart>> GetAllAsync()
        {
            return await _context.Carts.Include(x => x.Product).ToListAsync();
        }

        public async Task<Cart?> UpdateCartAsync(Cart cart)
        {
            var cartExist = await _context.Carts.Include(x => x.Product).FirstOrDefaultAsync(x => x.Id == cart.Id);
            if (cartExist == null)
            {
                return null;
            }
            cartExist.Quantity = cart.Quantity;
            await _context.SaveChangesAsync();
            return cartExist;
        }
    }
}