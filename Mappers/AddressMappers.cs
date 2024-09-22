using api.Dtos.Address;

namespace api.Mappers
{
    public static class AddressMappers
    {

        public static AddressDto ToAddressDto(this Address address)
        {
            return new AddressDto
            {
                Id = address.Id,
                ShippingName = address.ShippingName,
                FullName = address.FullName,
                ShippingPhone = address.ShippingPhone,
                StreetName = address.StreetName,
                BuildingNumber = address.BuildingNumber,
                HouseNumber = address.HouseNumber,
                CityOrArea = address.CityOrArea,
                District = address.District,
                Governorate = address.Governorate,
                NearestLandmark = address.NearestLandmark
            };
        }

        public static Address ToAddressCreateRequestDto(this AddressCreateRequestDto address, string userId)
        {
            return new Address
            {
                ShippingName = address.ShippingName,
                FullName = address.FullName,
                ShippingPhone = address.ShippingPhone,
                StreetName = address.StreetName,
                BuildingNumber = address.BuildingNumber,
                HouseNumber = address.HouseNumber,
                CityOrArea = address.CityOrArea,
                District = address.District,
                Governorate = address.Governorate,
                NearestLandmark = address.NearestLandmark,
                UserId = userId
            };
        }

        public static Address ToAddressUpdateRequestDto(this AddressUpdateRequestDto address, int id)
        {
            return new Address
            {
                Id = id,
                ShippingName = address.ShippingName,
                FullName = address.FullName,
                ShippingPhone = address.ShippingPhone,
                StreetName = address.StreetName,
                BuildingNumber = address.BuildingNumber,
                HouseNumber = address.HouseNumber,
                CityOrArea = address.CityOrArea,
                District = address.District,
                Governorate = address.Governorate,
                NearestLandmark = address.NearestLandmark
            };
        }


    }
}