using EShop.BLL.Service;
using EShop.DAL.Repository;
using EShop.DAL.utils;
using Microsoft.AspNetCore.Identity.UI.Services;


namespace EShop.PL
{
    public static class AppConfigaration
    {
        public static void Config(IServiceCollection Services)
        {
            Services.AddScoped<ICategoryRepository, CategoryRepository>();
            Services.AddScoped<ICategorySerivce, CategoryService>();
            Services.AddScoped<IAuthService, AuthService>();
            Services.AddScoped<ISeedData, RoleSeedData>();
            Services.AddScoped<ISeedData, UserSeadData>();
            Services.AddTransient<IEmailSender, EmailSender>();
        }
    }
}
