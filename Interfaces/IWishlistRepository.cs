using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface IWishlistRepository
    {
        Task<List<Wishlist>> GetAllAsync(string userId);
        Task<Wishlist?> CreateAsync(Wishlist wishlist);
        Task<Wishlist?> DeleteAsync(int id);
    }
}