namespace api.Interfaces
{
    public interface ISubCategoryRepository
    {
        Task<List<SubCategory>> GetAllAsync();

        Task<SubCategory?> GetSubCategoryByIdAsync(int id);

        Task<SubCategory?> CreateSubCategoryAsync(SubCategory subCategory);

        Task<SubCategory?> UpdateSubCategoryAsync(SubCategory subCategory);

        Task<SubCategory?> DeleteSubCategoryAsync(int id);

    }
}