using api.Data;

namespace api.Repositories
{
    public class AddressRepository(ApplicationDbContext _context) : IAddressRepository
    {
        public async Task<Address?> CreateAsync(Address address)
        {
            await _context.Addresses.AddAsync(address);
            await _context.SaveChangesAsync();
            return address;
        }

        public async Task<Address?> DeleteAsync(int id)
        {
            var address = await _context.Addresses.FirstOrDefaultAsync(x => x.Id == id);
            if (address == null)
            {
                return null;
            }
            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();
            return address;
        }

        public async Task<List<Address>> GetAllAsync(string userId)
        {
            return await _context.Addresses.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<Address?> UpdateAsync(Address address)
        {
            var addressExist = _context.Addresses.FirstOrDefault(x => x.Id == address.Id);
            if (addressExist == null)
            {
                return null;
            }
            addressExist.ShippingName = address.ShippingName;
            addressExist.FullName = address.FullName;
            addressExist.ShippingPhone = address.ShippingPhone;
            addressExist.StreetName = address.StreetName;
            addressExist.BuildingNumber = address.BuildingNumber;
            addressExist.HouseNumber = address.HouseNumber;
            addressExist.CityOrArea = address.CityOrArea;
            addressExist.District = address.District;
            addressExist.Governorate = address.Governorate;
            addressExist.NearestLandmark = address.NearestLandmark;
            await _context.SaveChangesAsync();
            return addressExist;
        }
    }
}