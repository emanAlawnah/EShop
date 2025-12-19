using EShop.BLL.Service;
using EShop.DAL.DTO.Request;
using EShop.DAL.Models;
using EShop.PL.Resourses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

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
        public IActionResult Index() {

            var response = _CategorySerivce.GetAllCategories();
            return Ok(new {massage= _Localizer["Success"].Value, response });
        
        }

       

    }
}
