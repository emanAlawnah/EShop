using EShop.DAL.DTO.Request;
using EShop.DAL.DTO.Response;
using EShop.DAL.Models;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EShop.BLL.Service
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AplecationUser> _UserManager;
        private readonly IConfiguration _Configuration;
        private readonly IEmailSender _EmailSender;
        private readonly SignInManager<AplecationUser> _Signinmaneger;

        public AuthService(UserManager<AplecationUser> userManager,IConfiguration configuration,IEmailSender emailSender, SignInManager<AplecationUser> signinmaneger)
        {
            _UserManager = userManager;
            _Configuration = configuration;
            _EmailSender = emailSender;
            _Signinmaneger = signinmaneger;
        }
        public async Task<LoginResponse> LoginAsync(LoginRequest loginRequest)
        {
            try
            {
                var user = await _UserManager.FindByEmailAsync(loginRequest.Email);
                if (user is null)
                {
                    return new LoginResponse()
                    {
                        Success = false,
                        Message = "invalid email"
                    };

                }

                if (await _UserManager.IsLockedOutAsync(user)) {
                    return new LoginResponse()
                    {
                        Success = true,
                        Message = "acount is locked, try agane later"
                    };
                }
                var result = await _Signinmaneger.CheckPasswordSignInAsync(user, loginRequest.Password, true);
                if (result.IsLockedOut)
                {
                    return new LoginResponse()
                    {
                        Success = true,
                        Message = "acount locked due to multiple atempts"
                    };
                }
               else if (result.IsNotAllowed)
                {
                    return new LoginResponse()
                    {
                        Success = false,
                        Message = "plz confairm your email"
                    };
                }

                else if (!result.Succeeded)
                {
                    return new LoginResponse()
                    {
                        Success = false,
                        Message = "none valied password"
                    };

                }
             



                return new LoginResponse()
                {
                    Success = true,
                    Message = "loged in succesfully",
                    AccessToken = await GenerateAcsessToken(user)
                };
            }

            catch (Exception ex)
            {
                return new LoginResponse()
                {

                    Success = false,
                    Message = "An unexpected Error",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<RegisterResponse> RegisterAsync(RegisterRequest registerRequest)
        {
            try
            {
                var user = registerRequest.Adapt<AplecationUser>();
                var result = await _UserManager.CreateAsync(user, registerRequest.Password);
                if (!result.Succeeded)
                {
                    return new RegisterResponse()
                    {
                        Success = false,
                        Message = "error, user creation failed",
                        Errors = result.Errors.Select(e => e.Description).ToList()

                    };

                }
                await _UserManager.AddToRoleAsync(user, "User");
                var token =  await _UserManager.GenerateEmailConfirmationTokenAsync(user);
                token = Uri.EscapeDataString(token);
                var emailUrl = $"https://localhost:7170/api/Auth/Acount/confairmemail?token={token}&userId={user.Id}";
                await _EmailSender.SendEmailAsync(user.Email,"confairm email", $@"
    <h2>Welcome {user.UserName}</h2>
    <p>Please confirm your email by clicking the link below:</p>
    <a href='{emailUrl}'>Confirm Email</a>
"

                    );
                return new RegisterResponse()
                 
                {
                    Success = true,
                    Message = "success"
                };


            }
            catch (Exception ex) {

                return new RegisterResponse()
                {
                    Success = false,
                    Message = "An unexpected Error",
                    Errors = new List<string> { ex.Message }

                };
            }
       
        }

        public async Task<bool> confairmEmailAsync(string token, string userId)
        {
            var user = await _UserManager.FindByIdAsync(userId);
            if (user is null) return false;

            var result = await _UserManager.ConfirmEmailAsync(user, token);
            if(!result.Succeeded) { return false; }
            return true;
        }
        public async Task<string> GenerateAcsessToken(AplecationUser user) {
            var userClaims = new List<Claim>()
        {
            new Claim ("id",user.Id),
            new Claim("username",user.UserName),
            new Claim ("email",user.Email)
        };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Configuration["Jwt:SecurityKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _Configuration["Jwt:Issuer"],
                audience: _Configuration["Jwt:Audience"],
                claims: userClaims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<ForgetPasswordResponse> ResetPasswordRequest(ForgetPasswordRequest request)
        {
            var user =await _UserManager.FindByEmailAsync(request.Email);

            if (user is null)
            {
                return new ForgetPasswordResponse
                {
                    Success = false,
                    Message = "Email Not Found"
                };

            }
            var Random = new Random();
            var code = Random.Next(1000,9999).ToString();
            user.CodeResetPassword = code;
            user.PasswordResetCodeExpiry = DateTime.UtcNow.AddMinutes(30);
            await _UserManager.UpdateAsync(user);
            await _EmailSender.SendEmailAsync(request.Email, "reset password", $"<p>code is {code} </p>");

            return new ForgetPasswordResponse
            {
                Success = true,
                Message = "code sent to your email"
            };

        }


        public async Task<ResetPasswordResponse> ResetPassword(ResetPasswordRequest request)
        {
            var user = await _UserManager.FindByEmailAsync(request.Email);

            if (user is null)
            {
                return new ResetPasswordResponse
                {
                    Success = false,
                    Message = "Email Not Found"
                };

            }

            else if (user.CodeResetPassword != request.Code) {
                return new ResetPasswordResponse
                {
                    Success = false,
                    Message = "invalied code"
                };
            }
            else if (user.PasswordResetCodeExpiry < DateTime.UtcNow)
            {
                return new ResetPasswordResponse
                {
                    Success = false,
                    Message = "code expierd"
                };
            }


            var token = await _UserManager.GeneratePasswordResetTokenAsync(user);
            var result = await _UserManager.ResetPasswordAsync(user,token, request.NewPassword);

            if (!result.Succeeded) {
                return new ResetPasswordResponse
                {
                    Success = false,
                    Message = "password reset faild",
                    Errors = result.Errors.Select(e => e.Description).ToList()
                };
            }
           
            await _EmailSender.SendEmailAsync(request.Email, "change password", $"<p>your hassword is changed </p>");

            return new ResetPasswordResponse
            {
                Success = true,
                Message = "password reset succesfully"
            };

        }

    }
}
