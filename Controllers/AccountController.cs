

namespace api.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountController(UserManager<BaseUser> userManager,
            ITokenService tokenService, SignInManager<BaseUser> signInManager, IEmailSenderService emailSender, IOtpService otpService, IStringLocalizer<AccountController> localizer) : ControllerBase
    {
        private readonly UserManager<BaseUser> _userManager = userManager;
        private readonly ITokenService _tokenService = tokenService;
        private readonly SignInManager<BaseUser> _signInManager = signInManager;
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
                return BadRequest(_localizer.GetString(AppStrings.emailAlreadyExist).Value);
            }

            var otp = _otpService.CreateOtpCode();
            var otpSecret = _otpService.CreateOtpSecret(checkEmailRequestDto.Email, otp);
            await _emailSender.SendEmailAsync(checkEmailRequestDto.Email, "OTP Verification", otp);

            return Ok(new NewOtpDto { Token = otpSecret });
        }

        [HttpPost]
        [Route("signup/customer")]
        [ApiExplorerSettings(GroupName = "v1-customer")]
        public async Task<IActionResult> SignupCustomer([FromBody] RegisterRequestDto registerRequestDto)
        {
            return await Register(registerRequestDto, ["User"], "Customer");
        }

        [HttpPost]
        // [Authorize(Roles = "Admin")]
        [Route("signup/admin")]
        [ApiExplorerSettings(GroupName = "v1-admin")]
        public async Task<IActionResult> SignupAdmin([FromBody] RegisterRequestDto registerRequestDto)
        {
            return await Register(registerRequestDto, ["Admin"], "Admin");
        }

        [HttpPost]
        [Route("signup/Delivery-Person")]
        [Authorize(Roles = "Admin")]
        [ApiExplorerSettings(GroupName = "v1-admin")]
        public async Task<IActionResult> Signup([FromBody] RegisterRequestDto registerRequestDto)
        {
            return await Register(registerRequestDto, ["DeliveryPerson"], "DeliveryPerson");
        }

        private async Task<IActionResult> Register(RegisterRequestDto registerRequestDto, List<string> roles, string userType)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var verifyEmail = _otpService.VerifyOtp(registerRequestDto.Email, registerRequestDto.OtpSecret, registerRequestDto.OtpCode);
            if (verifyEmail != null)
            {
                return BadRequest(verifyEmail);
            }

            BaseUser user = userType switch
            {
                "Admin" => new Admin
                {
                    Email = registerRequestDto.Email,
                    UserName = registerRequestDto.Username,
                    PhoneNumber = registerRequestDto.Phone,
                },
                "DeliveryPerson" => new DeliveryPerson
                {
                    Email = registerRequestDto.Email,
                    UserName = registerRequestDto.Username,
                    PhoneNumber = registerRequestDto.Phone,
                },
                _ => new Customer
                {
                    Email = registerRequestDto.Email,
                    UserName = registerRequestDto.Username,
                    PhoneNumber = registerRequestDto.Phone,
                },
            };
            var result = await _userManager.CreateAsync(user, registerRequestDto.Password);
            if (result.Succeeded)
            {
                var roleResult = await _userManager.AddToRolesAsync(user, roles);
                if (!roleResult.Succeeded)
                {
                    return StatusCode(500, roleResult.Errors);
                }
                var refreshToken = _tokenService.GenerateRefreshToken();
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
                await _userManager.UpdateAsync(user);
                return Ok(new NewUserDto { Token = _tokenService.CreateToken(user, [.. roles]), RefreshToken = refreshToken });
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
                return Unauthorized(_localizer.GetString(AppStrings.emailNotRegistered).Value);

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginRequestDto.Password, false);
            if (!result.Succeeded)
                return Unauthorized(_localizer.GetString(AppStrings.emailOrPasswordWrong).Value);
            var roles = await _userManager.GetRolesAsync(user);
            var refreshToken = _tokenService.GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
            await _userManager.UpdateAsync(user);
            return Ok(new NewUserDto { Token = _tokenService.CreateToken(user, [.. roles]), RefreshToken = refreshToken });
        }

        [HttpPost]
        [Route("logout")]
        [Authorize]
        public async Task<IActionResult> Logout(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                user.RefreshToken = null;
                await _userManager.UpdateAsync(user);
            }

            return Ok();
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            // Find the user by their refresh token
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.RefreshToken == request.RefreshToken);

            // Check if the user exists and the refresh token is still valid
            if (user == null || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return Unauthorized("Invalid or expired refresh token.");
            }

            // Get the user's role(s) from the UserManager
            var roles = await _userManager.GetRolesAsync(user);

            // Create new access token
            var newToken = _tokenService.CreateToken(user, roles.ToList());

            // Generate a new refresh token
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            // Update the refresh token in the database
            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
            await _userManager.UpdateAsync(user);

            // Return the new tokens
            return Ok(new NewUserDto
            {
                Token = newToken,
                RefreshToken = newRefreshToken
            });
        }

    }
}
