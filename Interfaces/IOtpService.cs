namespace api.Interfaces
{
    public interface IOtpService
    {
        public string CreateOtpSecret(string email,string otp);

        public string?  VerifyOtp(string email, string otpSecret, string otp);

        public string CreateOtpCode(int length = 4);
    }
}