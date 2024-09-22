namespace api.Interfaces
{
    public interface IPaymentService
    {
        public  Task<string> CreatePaymentSession(Dictionary<string, string> data);
    }
}