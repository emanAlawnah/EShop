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
        [HttpPost("Regiser")]
        public async Task<IActionResult> Register(RegisterRequest Request)
        {
            var result = await _AuthenticationService.RegisterAsync(Request);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

    }
}
