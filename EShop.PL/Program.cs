

using EShop.BLL.Service;
using EShop.DAL.Data;
using EShop.DAL.Models;
using EShop.DAL.Repository;
using EShop.DAL.utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Globalization;
using System.Threading.Tasks;

namespace EShop.PL
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddLocalization(options => options.ResourcesPath = "");

            const string defaultCulture = "en";

            var supportedCultures = new[]
            {
                    new CultureInfo(defaultCulture),
                    new CultureInfo("ar"),
                };

            builder.Services.Configure<RequestLocalizationOptions>(options => {
                options.DefaultRequestCulture = new RequestCulture(defaultCulture);
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
                options.RequestCultureProviders.Clear();
                options.RequestCultureProviders.Add(new QueryStringRequestCultureProvider{
                    QueryStringKey="lang"
                });
            });


            builder.Services.AddDbContext<AplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings")["DefultConection"]));

            builder.Services.AddIdentity<AplecationUser, IdentityRole>().AddEntityFrameworkStores<AplicationDbContext>().AddDefaultTokenProviders();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<ICategorySerivce, CategoryService>();

            builder.Services.AddScoped<ISeedData, RoleSeedData>();
            builder.Services.AddScoped<ISeedData, UserSeadData>();

            var app = builder.Build();
            app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            using (var scop = app.Services.CreateScope()) {
                var Services = scop.ServiceProvider;
                var seeders = Services.GetServices<ISeedData>();
                foreach (var seeder in seeders) {
                    await seeder.DAtaSeed();
                }
            }
            app.MapControllers();

            app.Run();
        }
    }
}
