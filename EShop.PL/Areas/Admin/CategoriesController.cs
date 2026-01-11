using EShop.BLL.Service;
using EShop.DAL.DTO.Request;
using EShop.PL.Resourses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Security.Claims;

namespace EShop.PL.Areas.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize (Roles ="Admin")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategorySerivce _CategorySerivce;
        private readonly IStringLocalizer<SharedResource> _Localizer;

        public CategoriesController(ICategorySerivce categorySerivce, IStringLocalizer<SharedResource> localizer)
        {
            _CategorySerivce = categorySerivce;
            _Localizer = localizer;
        }

        [HttpPost("")]
        public IActionResult Create(CategoryRequest request)
        {
            
            var response = _CategorySerivce.CreateCategory(request);
            return Ok(new { massage = _Localizer["Success"].Value });

        }
    }
}
