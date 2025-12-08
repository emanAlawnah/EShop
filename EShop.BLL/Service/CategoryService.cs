using EShop.DAL.DTO.Request;
using EShop.DAL.DTO.Response;
using EShop.DAL.Models;
using EShop.DAL.Repository;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.BLL.Service
{
    public class CategoryService : ICategorySerivce
    {
        private readonly ICategoryRepository _CategoryRepository;

        public CategoryService(ICategoryRepository categoryRepository )
        {
            _CategoryRepository = categoryRepository;
        }


        public CategoryResponse CreateCategory(CategoryRequest request)
        {
            var category=request.Adapt<Category>();
            _CategoryRepository.Create(category);
            return category.Adapt<CategoryResponse>();
        }

      

        public List<CategoryResponse> GetAllCategories()
        {
            var Categories = _CategoryRepository.GetAll();
            var Response = Categories.Adapt<List<CategoryResponse>>();
            return Response;
        }
    }
}
