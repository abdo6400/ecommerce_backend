using System.Threading.Tasks;
using api.Dtos.Account;
using api.Interfaces;
using api.Models;
using api.Resources;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace api.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountController(UserManager<AppUser> userManager,
            ITokenService tokenService, SignInManager<AppUser> signInManager, IEmailSenderService emailSender, IOtpService otpService, IStringLocalizer<AccountController> localizer) : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly ITokenService _tokenService = tokenService;
        private readonly SignInManager<AppUser> _signInManager = signInManager;
        private readonly IEmailSenderService _emailSender = emailSender;
        private readonly IOtpService _otpService = otpService;
        private readonly IStringLocalizer<AccountController> _localizer = localizer;

        [HttpPost("checkEmail")]
        public async Task<IActionResult> CheckEmail([FromBody] CheckEmailRequestDto checkEmailRequestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(checkEmailRequestDto.Email);
            if (user != null)
            {
                return BadRequest(_localizer.GetString(AppStrings.emailAlreadyExist));
            }

            var otp = _otpService.CreateOtpCode();
            var otpSecret = _otpService.CreateOtpSecret(checkEmailRequestDto.Email, otp);
            await _emailSender.SendEmailAsync(checkEmailRequestDto.Email, "OTP Verification", otp);

            return Ok(new NewOtpDto { Token = otpSecret });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var verifyEmail = _otpService.VerifyOtp(registerRequestDto.Email, registerRequestDto.OtpSecret, registerRequestDto.OtpCode);
            if (verifyEmail != null)
            {
                return BadRequest(verifyEmail);
            }

            var user = new AppUser
            {
                Email = registerRequestDto.Email,
                UserName = registerRequestDto.Username,
                PhoneNumber = registerRequestDto.Phone,
            };

            var result = await _userManager.CreateAsync(user, registerRequestDto.Password);
            if (result.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(user, "User");
                if (!roleResult.Succeeded)
                {
                    return StatusCode(500, roleResult.Errors);
                }

                return Ok(new NewUserDto { Token = _tokenService.CreateToken(user) });
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == loginRequestDto.Email);
            if (user == null)
                return Unauthorized(_localizer.GetString(AppStrings.emailNotRegistered));

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginRequestDto.Password, false);
            if (!result.Succeeded)
                return Unauthorized(_localizer.GetString(AppStrings.emailOrPasswordWrong));

            return Ok(new NewUserDto { Token = _tokenService.CreateToken(user) });
        }
    }
}
