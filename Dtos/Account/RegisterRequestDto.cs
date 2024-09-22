namespace api.Dtos.Account
{
    public class RegisterRequestDto
    {
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string OtpCode { get; set; } = null!;
        public string OtpSecret { get; set; } = null!;
    }
}