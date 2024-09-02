using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart?> CreateCartAsync(Cart cart);
        Task<Cart?> DeleteCartAsync(int id);
        Task<List<Cart>> GetAllAsync();
        Task<Cart?> UpdateCartAsync(Cart cart);
    }
}