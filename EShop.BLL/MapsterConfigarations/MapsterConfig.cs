using EShop.DAL.DTO.Response;
using EShop.DAL.Models;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.BLL.MapsterConfigarations
{
    public static class MapsterConfig
    {
        public static void MapsterCoinfRegister()
        {
            //TypeAdapterConfig<Category, CategoryResponse>.NewConfig()
            //    .Map(dest => dest.CategoryId, source => source.Id);

            TypeAdapterConfig<Category, CategoryResponse>.NewConfig()
                .Map(dest => dest.UserCreated, source => source.User.UserName);


            TypeAdapterConfig<Category, CategoryUserResponse>.NewConfig()
              .Map(dest => dest.Name, source => source.Translations.Where(t => t.Language == MapContext.Current.Parameters["lang"].ToString()).Select(t => t.Name).FirstOrDefault());

            TypeAdapterConfig<Product,ProductResponse>.NewConfig()
                .Map(dest=>dest.MainImage, source => $"https://localhost:7170/Images/{source.MainImage}"); 
        }

       
    }
}
