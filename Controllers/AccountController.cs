using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Account;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountController(UserManager<AppUser> _userManager,
                ITokenService _tokenService, SignInManager<AppUser> _signInManager, IEmailSenderService _emailSender, IOtpService _otpService) : ControllerBase
    {
        [HttpPost]
        [Route("CheckEmail")]
        public async Task<IActionResult> CheckEmail([FromBody] CheckEmailRequestDto checkEmailRequestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var user = await _userManager.FindByEmailAsync(checkEmailRequestDto.Email);
            if (user != null)
            {
                return BadRequest("Email already exists");
            }
            var otp = _otpService.CreateOtpCode();
            var otpSecret = _otpService.CreateOtpSecret(checkEmailRequestDto.Email, otp);
            await _emailSender.SendEmailAsync(checkEmailRequestDto.Email, "otp", otp);
            return Ok(
                new NewOtpDto
                {
                    OtpSecret = otpSecret
                }
            );
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var verfiyEmail = _otpService.VerifyOtp(registerRequestDto.Email,
                registerRequestDto.OtpSecret, registerRequestDto.OtpCode);
                if (verfiyEmail != null)
                {
                    return BadRequest(verfiyEmail);
                }

                var user = new AppUser
                {
                    Email = registerRequestDto.Email,
                    UserName = registerRequestDto.Username,
                    PhoneNumber = registerRequestDto.Phone
                };

                var result = await _userManager.CreateAsync(user, registerRequestDto.Password);
                if (result.Succeeded)
                {
                    var role = await _userManager.AddToRoleAsync(user, "User");

                    if (!role.Succeeded)
                        return StatusCode(500, role.Errors);

                    return Ok(new NewUserDto
                    {
                        Token = _tokenService.CreateToken(user),
                    });
                }
                return BadRequest(result.Errors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == loginRequestDto.Email.ToLower());
                if (user == null)
                    return Unauthorized("UserName Not Found!");
                var result = await _signInManager.CheckPasswordSignInAsync(user, loginRequestDto.Password, false);

                if (!result.Succeeded)
                    return Unauthorized("UserName or Password is incorrect!");

                return Ok(new NewUserDto
                {

                    Token = _tokenService.CreateToken(user),
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}