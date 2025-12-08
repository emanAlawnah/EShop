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
        List<CategoryResponse> GetAllCategories();
        CategoryResponse CreateCategory(CategoryRequest category);
    }
}
