namespace api.Dtos.Address
{
    public class AddressDto
    {
        public int Id { get; set; }
        public string? ShippingName { get; set; }
        public string FullName { get; set; } = null!;
        public string ShippingPhone { get; set; } = null!;
        public string StreetName { get; set; } = null!;
        public string? BuildingNumber { get; set; }
        public string? HouseNumber { get; set; }
        public string CityOrArea { get; set; } = null!;
        public string? District { get; set; }
        public string Governorate { get; set; } = null!;
        public string? NearestLandmark { get; set; }
    }
}