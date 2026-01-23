using EShop.BLL.Service;
using EShop.DAL.DTO.Request;
using EShop.PL.Resourses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace EShop.PL.Areas.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    [Authorize (Roles ="Admin")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _ProductService;
        private readonly IStringLocalizer<SharedResource> _Localizer;

        public ProductsController(IProductService productService,IStringLocalizer<SharedResource> localizer)
        {
            _ProductService = productService;
            _Localizer = localizer;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var response = await _ProductService.GetAllProductsForAdmin();
            return Ok(new { massage = _Localizer["Success"].Value, response });
        }

        [HttpPost("")]
        public async Task<IActionResult> create([FromForm]ProductRequest request)
        {
          var response = await _ProductService.CreateProduct(request);
          return Ok(new { massage = _Localizer["Success"].Value, response });
        }
        
    }
}
