using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using api.Interfaces;
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
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, otp),
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
                var tokenOtp = jwtToken.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Sub).Value;
                var tokenEmail = jwtToken.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Email).Value;

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
}