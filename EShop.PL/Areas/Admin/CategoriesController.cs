using EShop.BLL.Service;
using EShop.DAL.DTO.Request;
using EShop.PL.Resourses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Security.Claims;

namespace EShop.PL.Areas.Admin
{
    [Route("api/admin/[controller]")]
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
        public async Task <IActionResult> Create([FromBody]CategoryRequest request)
        {
            
            var response =  await _CategorySerivce.CreateCategory(request);
            return Ok(new { massage = _Localizer["Success"].Value });

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute]int id)
        {
            var result = await _CategorySerivce.DeleteCategoryAsync(id);
            if (!result.Success)
            {
             if(result.Message.Contains("not found"))
                {
                    return NotFound(result);
                }
             return BadRequest();
            }

            return Ok(result);
        }

        [HttpPatch("toggle-status/{id}")]
        public async Task<IActionResult> ToggleStatus (int id)
        {
            var result = await _CategorySerivce.ToggleStatus(id);
            if (!result.Success)
            {
                if (result.Message.Contains("not found"))
                {
                    return NotFound(result);
                }
                return BadRequest();
            }

            return Ok(result);

        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {


            var response = await _CategorySerivce.GetAllCategoriesForAdmin();
            return Ok(new { massage = _Localizer["Success"].Value, response });

        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UbdateCategory([FromRoute]int id,[FromBody] CategoryRequest request)
        {
            var result = await _CategorySerivce.UpdateCategoryAsync(id, request);
            if (!result.Success)
            {
                if (result.Message.Contains("not found"))
                {
                    return NotFound(result);
                }
                return BadRequest();
            }

            return Ok(result);
        }
    }
}
