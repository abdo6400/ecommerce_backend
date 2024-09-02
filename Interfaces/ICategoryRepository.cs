using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category?> CreateCategoryAsync(Category category);
        Task<Category?> DeleteCategoryAsync(int id);
        Task<List<Category>> GetAllAsync();
        Task<Category?> GetCategoryByIdAsync(int id);
        Task<Category?> UpdateCategoryAsync(Category category);
    }
}