namespace api.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(BaseUser user, List<string> roles);

        string GenerateRefreshToken();
    }
}