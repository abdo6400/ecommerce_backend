namespace api.Models
{
    public class DeliveryPerson : BaseUser
    {
        public List<Delivery> Deliveries { get; set; } = [];
    }
}