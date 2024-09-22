namespace api.Interfaces
{
    public interface IWishlistRepository
    {
        Task<List<Wishlist>> GetAllAsync(string userId);
        Task<Wishlist?> CreateAsync(Wishlist wishlist);
        Task<Wishlist?> DeleteAsync(int id);
    }
}