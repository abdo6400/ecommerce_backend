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
    public class SubCategoryRepository(ApplicationDbContext _context) : ISubCategoryRepository
    {
        public async Task<SubCategory?> CreateSubCategoryAsync(SubCategory subCategory)
        {
            await _context.SubCategories.AddAsync(subCategory);
            await _context.SaveChangesAsync();
            return subCategory;
        }

        public async Task<SubCategory?> DeleteSubCategoryAsync(int id)
        {
            var subCategory = await _context.SubCategories.FirstOrDefaultAsync(x => x.Id == id);
            if (subCategory == null)
            {
                return null;
            }
            _context.SubCategories.Remove(subCategory);
            await _context.SaveChangesAsync();
            return subCategory;
        }

        public async Task<List<SubCategory>> GetAllAsync()
        {
            return await _context.SubCategories.ToListAsync();
        }

        public async Task<SubCategory?> GetSubCategoryByIdAsync(int id)
        {
            return await _context.SubCategories.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<SubCategory?> UpdateSubCategoryAsync(SubCategory subCategory)
        {
            var subCategoryExist = await _context.SubCategories.FirstOrDefaultAsync(x => x.Id == subCategory.Id);
            if (subCategoryExist == null)
            {
                return null;
            }
            subCategoryExist.Name = subCategory.Name;
            subCategoryExist.Image = subCategory.Image;
            await _context.SaveChangesAsync();
            return subCategoryExist;
        }
    }
}