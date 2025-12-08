using EShop.BLL.Service;
using EShop.DAL.Data;
using EShop.DAL.DTO.Request;
using EShop.DAL.DTO.Response;
using EShop.DAL.Models;
using EShop.DAL.Repository;
using EShop.PL.Resourses;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace EShop.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IStringLocalizer<SharedResource> _Localizer;
        private readonly ICategorySerivce _CategorySerivce;

        public CategoriesController(IStringLocalizer<SharedResource> localizer, ICategorySerivce categorySerivce) {
            _Localizer = localizer;
            _CategorySerivce = categorySerivce;
        }

        [HttpGet("")]
        public IActionResult index() {
            var response= _CategorySerivce.GetAllCategories();


            return Ok(new {massage=_Localizer["Success"].Value, response });   
        }

        [HttpPost("")]
        public IActionResult Create(CategoryRequest request)
        {
            var response=_CategorySerivce.CreateCategory(request);
            return Ok(new { massage = _Localizer["Success"].Value });
        }

    }
}
