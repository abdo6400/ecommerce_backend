using api.Data;

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
            subCategoryExist.NameEn = subCategory.NameEn;
            subCategoryExist.NameAr = subCategory.NameAr;
            subCategoryExist.Image = subCategory.Image;
            await _context.SaveChangesAsync();
            return subCategoryExist;
        }
    }
}