using EShop.DAL.Data;
using EShop.DAL.DTO.Request;
using EShop.DAL.DTO.Response;
using EShop.DAL.Models;
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
        private readonly AplicationDbContext _context;
        private readonly IStringLocalizer<SharedResource> _Localizer;

        public CategoriesController(AplicationDbContext Context,IStringLocalizer<SharedResource> localizer) {
            _context = Context;
            _Localizer = localizer;
        }

        [HttpGet("")]
        public IActionResult index() {
            var Categories = _context.Categories.Include(c=>c.Translations).ToList();
            var Response = Categories.Adapt<List<CategoryResponse>>();

            
            return Ok(new {massage=_Localizer["Success"].Value, Response });   
        }

        [HttpPost("")]
        public IActionResult Create(CategoryRequest request)
        {
            var cat = request.Adapt<Category>();
            _context.Categories.Add(cat);
            _context.SaveChanges();

            return Ok(new { massage = _Localizer["Success"].Value });
        }

    }
}
