namespace api.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllAsync(string userId);
        Task<Order?> CreateAsync(string userId, int addressId, string? code, string paymentMethod, string? transactionId = null);
        Task<Order?> DeleteAsync(int id);
    }
}