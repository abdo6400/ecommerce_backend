namespace api.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart?> CreateCartAsync(Cart cart);
        Task<Cart?> DeleteCartAsync(int id);
        Task<List<Cart>> GetAllAsync(string userId);
        Task<Cart?> UpdateCartAsync(Cart cart);
    }
}