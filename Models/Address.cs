namespace api.Models
{
    public class Address
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
        public string UserId { get; set; } = null!;

        // Navigation properties
        public List<Order> Orders { get; set; } = null!;

        public Customer User { get; set; } = null!;
    }
}