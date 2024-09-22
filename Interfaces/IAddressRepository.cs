namespace api.Interfaces
{
    public interface IAddressRepository
    {
        Task<List<Address>> GetAllAsync(string userId);

        Task<Address?> CreateAsync(Address address);

        Task<Address?> UpdateAsync(Address address);

        Task<Address?> DeleteAsync(int id);

    }
}