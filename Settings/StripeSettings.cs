namespace api.Settings
{
    public class StripeSettings
    {
        public string SecretKey { get; set; } = null!;
        public string PublishableKey { get; set; } = null!;

        public string StripeSignature { get; set; } = null!;
    }
}