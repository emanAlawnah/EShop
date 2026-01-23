using EShop.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DAL.Data
{
    public class ApplicationDbContext :IdentityDbContext<AplecationUser>
    {
        private readonly IHttpContextAccessor _HttpContextAccessor;

        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryTranslation> CategoryTranslations { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductTranslation> ProductTranslations { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
            IHttpContextAccessor httpContextAccessor
            )
        : base(options)
        {
            _HttpContextAccessor = httpContextAccessor;
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<AplecationUser>().ToTable("USers");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaim");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");

            builder.Entity<Category>()
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.CreatedBy)
                .OnDelete(DeleteBehavior.NoAction);



        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<BaseModel>();
            if(_HttpContextAccessor.HttpContext != null) {
             var currentId = _HttpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                foreach (var entry in entries)
            {

                if (entry.State == EntityState.Added)
                {
                    entry.Property(x => x.CreatedBy).CurrentValue = currentId;
                    entry.Property(x => x.CreateAt).CurrentValue = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Modified)
                {

                    entry.Property(x => x.UpdatedBy).CurrentValue = currentId;
                    entry.Property(x => x.UpdatedAt).CurrentValue = DateTime.UtcNow;
                }
            }
            }
            return base.SaveChangesAsync(cancellationToken);


        }
        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries<BaseModel>();
            var currentId = _HttpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            foreach (var entry in entries) {

                if (entry.State == EntityState.Added)
                {
                    entry.Property(x => x.CreatedBy).CurrentValue = currentId;
                    entry.Property(x => x.CreateAt).CurrentValue = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Modified) {

                    entry.Property(x => x.UpdatedBy).CurrentValue = currentId;
                    entry.Property(x => x.UpdatedAt).CurrentValue = DateTime.UtcNow;
                }
            }
            return base.SaveChanges();
        }

    }
}
