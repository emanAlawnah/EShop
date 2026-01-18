using EShop.DAL.DTO.Request;
using EShop.DAL.DTO.Response;
using EShop.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.BLL.Service
{
    public interface ICategorySerivce
    {
        Task<BaseResponse> ToggleStatus(int id);
        Task<BaseResponse> DeleteCategoryAsync(int id);
        Task<List<CategoryResponse>> GetAllCategoriesForAdmin();
        Task<List<CategoryUserResponse>> GetAllCategoriesForUser(string lang = "en");
        Task<CategoryResponse> CreateCategory(CategoryRequest request);
        Task<BaseResponse> UpdateCategoryAsync(int id, CategoryRequest request);
    }
}
