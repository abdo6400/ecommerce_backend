using api.Data;

namespace api.Repositories
{
    public class BrandRepository(ApplicationDbContext _context) : IBrandRepository
    {
        public async Task<Brand?> CreateBrandAsync(Brand brand)
        {
            await _context.Brands.AddAsync(brand);
            await _context.SaveChangesAsync();
            return brand;
        }

        public async Task<Brand?> DeleteBrandAsync(int id)
        {
            var brand = await _context.Brands.FirstOrDefaultAsync(x => x.Id == id);
            if (brand == null)
            {
                return null;
            }
            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync();
            return brand;
        }

        public async Task<List<Brand>> GetAllAsync()
        {
            return await _context.Brands.ToListAsync();

        }

        public async Task<Brand?> GetBrandByIdAsync(int id)
        {
            return await _context.Brands.FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<Brand?> UpdateBrandAsync(Brand brand)
        {
            var brandExist = await _context.Brands.FirstOrDefaultAsync(x => x.Id == brand.Id);
            if (brandExist == null)
            {
                return null;
            }
            brandExist.NameAr = brand.NameAr;
            brandExist.NameEn = brand.NameEn;
            brandExist.Image = brand.Image;
            await _context.SaveChangesAsync();
            return brandExist;
        }
    }
}