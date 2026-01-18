using EShop.BLL.Service;
using EShop.DAL.DTO.Request;
using EShop.DAL.Models;
using EShop.PL.Resourses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EShop.PL.Areas.User
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class CategoriesController : ControllerBase
    {
        private readonly ICategorySerivce _CategorySerivce;
        private readonly IStringLocalizer<SharedResource> _Localizer;

        public CategoriesController(ICategorySerivce categorySerivce,IStringLocalizer<SharedResource> localizer)
        {
            _CategorySerivce = categorySerivce;
            _Localizer = localizer;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index([FromQuery] string lang = "en") {
            
           
            var response = await _CategorySerivce.GetAllCategoriesForUser(lang);
            return Ok(new {massage= _Localizer["Success"].Value, response });
        
        }



    }
}
