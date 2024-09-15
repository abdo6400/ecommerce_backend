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
    public class WishlistRepository(ApplicationDbContext _context) : IWishlistRepository
    {
        public async Task<Wishlist?> CreateAsync(Wishlist wishlist)
        {
            if (await _context.Products.AnyAsync(x => x.Id == wishlist.ProductId))
            {
                var existingWishlist = await _context.Wishlists.FirstOrDefaultAsync(w => w.ProductId == wishlist.ProductId && w.UserId == wishlist.UserId);

                if (existingWishlist != null)
                {
                    return existingWishlist;
                }

                await _context.Wishlists.AddAsync(wishlist);
                await _context.SaveChangesAsync();
                var createdWishlist = await _context.Wishlists
                    .Include(w => w.Product)
                    .FirstOrDefaultAsync(w => w.Id == wishlist.Id);
                return createdWishlist;
            }
            return null;
        }

        public async Task<Wishlist?> DeleteAsync(int id)
        {
            var wishlist = await _context.Wishlists.Include(x => x.Product).FirstOrDefaultAsync(x => x.ProductId == id);

            if (wishlist == null)
            {
                return null;
            }
            _context.Wishlists.Remove(wishlist);
            await _context.SaveChangesAsync();
            return wishlist;
        }

        public async Task<List<Wishlist>> GetAllAsync(string userId)
        {
            return await _context.Wishlists.Include(x => x.Product).Where(x => x.UserId == userId).ToListAsync();
        }
    }
}