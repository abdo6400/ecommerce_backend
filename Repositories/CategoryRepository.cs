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
    public class CategoryRepository(ApplicationDbContext _context) : ICategoryRepository
    {
        public async Task<Category?> CreateCategoryAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<Category?> DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
            {
                return null;
            }
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories.Include(x => x.SubCategories).ThenInclude(sc => sc.Brands)
                .ThenInclude(b => b.Products)
                   .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Category?> UpdateCategoryAsync(Category category)
        {
            var categoryExist = await _context.Categories.FirstOrDefaultAsync(x => x.Id == category.Id);
            if (categoryExist == null)
            {
                return null;
            }
            categoryExist.NameEn = category.NameEn;
            categoryExist.NameAr = category.NameAr;
            categoryExist.Image = category.Image;
            await _context.SaveChangesAsync();
            return categoryExist;
        }
    }
}