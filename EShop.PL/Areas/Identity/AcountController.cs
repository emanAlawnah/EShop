using EShop.BLL.Service;
using EShop.DAL.DTO.Request;
using EShop.DAL.DTO.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
namespace EShop.PL.Areas.Identity
{
    [Route("api/Auth/[controller]")]
    [ApiController]
    public class AcountController : ControllerBase
    {
        private readonly IAuthService _AuthenticationService;

        public AcountController(IAuthService authenticationService)
        {
            _AuthenticationService = authenticationService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest Request)
        {
            var result = await _AuthenticationService.LoginAsync(Request);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);

        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterRequest Request)
        {
            var result = await _AuthenticationService.RegisterAsync(Request);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("confairmemail")]
        public async Task<IActionResult> ConfairmEmail (string token, string userId)
        {
            var result = await _AuthenticationService.confairmEmailAsync(token, userId);
       
            return Ok(result);
        }

        [HttpPost("SendCode")]
        public async Task<IActionResult> RequestPasswordReset(ForgetPasswordRequest request) {
            var result = await _AuthenticationService.ResetPasswordRequest(request);
            if (!result.Success) { 
            return BadRequest(result);
            }
            return Ok(result);
        }



        [HttpPatch("resetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            var result = await _AuthenticationService.ResetPassword(request);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPatch("RefreshToken")]
        public async Task<IActionResult> RefreshToken(TokenApiModel request)
        {
         var result = await _AuthenticationService.RefreshTokenAsync(request);
            if (!result.Success) 
            {
                return BadRequest(result);
            } else
                return Ok(result);
        }
    }
}
