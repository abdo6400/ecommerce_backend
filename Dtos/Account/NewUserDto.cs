namespace api.Dtos.Account
{
    public class NewUserDto
    {
        public string Token { get; set; } = "";

        public string RefreshToken { get; set; } = "";
    }
}