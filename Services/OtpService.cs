using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace api.Services
{
    public class OtpService : IOtpService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;

        public OtpService(IConfiguration config)
        {
            _config = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SiginKey"]!));
        }

        public string CreateOtpSecret(string email, string otp)
        {
            var encryptedOtp = EncryptionHelper.Encrypt(otp);

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, encryptedOtp),
                new(JwtRegisteredClaimNames.Email, email),
            };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(5),
                SigningCredentials = creds,
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"]
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string? VerifyOtp(string email, string otpSecret, string otp)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = _key,
                    ValidateIssuer = true,
                    ValidIssuer = _config["JWT:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = _config["JWT:Audience"],
                    ClockSkew = TimeSpan.Zero // Minimizes token expiration timing issues
                };

                ClaimsPrincipal principal = tokenHandler.ValidateToken(otpSecret, tokenValidationParameters, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                // Extract the OTP and email from the token's claims
                var encryptedOtp = jwtToken.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Sub).Value;
                var tokenEmail = jwtToken.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Email).Value;

                // Decrypt the OTP
                var tokenOtp = EncryptionHelper.Decrypt(encryptedOtp);

                // Ensure the token is not expired (additional check)
                var expiration = jwtToken.ValidTo;
                if (DateTime.UtcNow > expiration)
                {
                    return "OTP has expired.";
                }
                if (tokenEmail != email)
                {
                    return "Invalid Email";
                }
                if (tokenOtp != otp)
                {
                    return "Invalid OTP";
                }
                return null;

            }
            catch (SecurityTokenExpiredException)
            {
                return "OTP has expired.";
            }
            catch (SecurityTokenException)
            {
                return "Invalid OTP token.";
            }
            catch (Exception)
            {
                return "An unexpected error occurred during OTP verification.";
            }
        }

        public string CreateOtpCode(int length = 4)
        {
            const string chars = "0123456789";
            StringBuilder otp = new(length);
            byte[] randomBytes = new byte[length];
            RandomNumberGenerator.Fill(randomBytes);

            for (int i = 0; i < length; i++)
            {
                otp.Append(chars[randomBytes[i] % chars.Length]);
            }

            return otp.ToString();
        }
    }

    public static class EncryptionHelper
    {
        // Replace these with your actual base64-encoded key and IV
        private static readonly byte[] Key = Convert.FromBase64String("G8kDFaXhCjY/3M4kEkwA9FgM4WbU7xM7b6F3Q5Z3uFg=");
        private static readonly byte[] Iv = Convert.FromBase64String("bH1zv6KNyKzj9V9Ih8z2hg==");

        public static string Encrypt(string plainText)
        {
            using var aes = Aes.Create();
            aes.Key = Key;
            aes.IV = Iv;

            var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using var memoryStream = new MemoryStream();
            using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            using var writer = new StreamWriter(cryptoStream);

            writer.Write(plainText);
            writer.Flush();
            cryptoStream.FlushFinalBlock();

            return Convert.ToBase64String(memoryStream.ToArray());
        }

        public static string Decrypt(string cipherText)
        {
            using var aes = Aes.Create();
            aes.Key = Key;
            aes.IV = Iv;

            var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using var memoryStream = new MemoryStream(Convert.FromBase64String(cipherText));
            using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            using var reader = new StreamReader(cryptoStream);

            return reader.ReadToEnd();
        }
    }
}
