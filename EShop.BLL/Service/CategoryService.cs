using EShop.DAL.DTO.Request;
using EShop.DAL.DTO.Response;
using EShop.DAL.Models;
using EShop.DAL.Repository;
using Mapster;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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


        public async Task<CategoryResponse> CreateCategory(CategoryRequest request)
        {
            var category=request.Adapt<Category>();
            await _CategoryRepository.CreateAsync(category);
            return category.Adapt<CategoryResponse>();
        }

        public async Task<BaseResponse> ToggleStatus(int id) {
            try
            {
                var category = await _CategoryRepository.FindByIdAsync(id);
                if (category is null)
                {
                    return new BaseResponse
                    {
                        Success = false,
                        Message = "category not found"
                    };
                }
                category.Status = category.Status == Status.Active ? Status.InActive : Status.Active;
                await  _CategoryRepository.UpdateAsync(category);

                return new BaseResponse
                {
                    Success = true,
                    Message = "category updated sucessfully"
                };
            }

            catch (Exception ex)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "cant change category status",
                    Errors = new List<string> { ex.Message }
                };
            }


        }

        public async Task<List<CategoryResponse>> GetAllCategoriesForAdmin()
        {
            var Categories = await _CategoryRepository.GetAllAsync();
            var Response = Categories.Adapt<List<CategoryResponse>>();
            return Response;
        }

        public async Task<List<CategoryUserResponse>> GetAllCategoriesForUser(string lang = "en")
        {
            var Categories = await _CategoryRepository.GetAllAsync();
            //   foreach (var category in Categorise)
            //   {
            //       category.Translations = category.Translations.Where(t => t.Language == lang).ToList();
            //   }
            //var Response = Categorise.Select(c => new CategoryUserResponse
            //{
            //    Id = c.Id,
            //    Name = c.Translations.Where(t => t.Language == lang).Select(t => t.Name).FirstOrDefault()
            //}).ToList();
            var Response = Categories.BuildAdapter().AddParameters("lang",lang).AdaptToType<List<CategoryUserResponse>>();

            return Response;
        }

        public async Task<BaseResponse> DeleteCategoryAsync(int id)
        {
            try
            {
                var category = await _CategoryRepository.FindByIdAsync(id);
                if (category is null)
                {
                    return new BaseResponse
                    {
                        Success = false,
                        Message = "category not found"
                    };
                }
                await _CategoryRepository.DeleteAsync(category);
                return new BaseResponse
                {
                    Success = true,
                    Message = "caregory deleted"
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "cant delete category",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task <BaseResponse> UpdateCategoryAsync(int id,CategoryRequest request)
        {
            try
            {
                var category = await _CategoryRepository.FindByIdAsync(id);
                if (category is null)
                {
                    return new BaseResponse
                    {
                        Success = false,
                        Message = "Category not found"

                    };
                }
                if (request.Translations != null)
                {
                    foreach (var translation in request.Translations)
                    {
                        var existing = category.Translations.FirstOrDefault(t => t.Language == translation.Language);
                        if (existing is not null)
                        {
                            existing.Name = translation.Name;
                        }
                        else
                        {
                            return new BaseResponse
                            {
                                Success = true,
                                Message = $"language {translation.Language} not suported"
                            };
                        }
                    }
                    await _CategoryRepository.UpdateAsync(category);
                    
                }
                return new BaseResponse
                {
                    Success = true,
                    Message = "Category updated sucessfully"
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Cant update Category",
                    Errors = new List<string> { ex.Message }
                };
            }
        }
        

        }
    }

