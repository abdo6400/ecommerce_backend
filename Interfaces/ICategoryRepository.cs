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