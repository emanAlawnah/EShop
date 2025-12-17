using EShop.DAL.DTO.Request;
using EShop.DAL.DTO.Response;
using EShop.DAL.Models;
using Mapster;
using Microsoft.AspNetCore.Identity;
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

        public AuthService(UserManager<AplecationUser> userManager,IConfiguration configuration)
        {
            _UserManager = userManager;
            _Configuration = configuration;
        }
        public async Task<LoginResponse> LoginAsync(LoginRequest loginRequest)
        {
            try
            {
                var user = await _UserManager.FindByEmailAsync(loginRequest.Email);
                if (user is null)
                {
                    return new LoginResponse
                    {
                        Success = false,
                        Message = "invalid email"
                    };

                }

                var result = await _UserManager.CheckPasswordAsync(user, loginRequest.Password);
                if (!result)
                {
                    return new LoginResponse
                    {
                        Success = false,
                        Message = "invalid pasword"
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
    }
}
