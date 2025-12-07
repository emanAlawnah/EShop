using EShop.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DAL.Data
{
    public class AplicationDbContext :DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryTranslation> CategoryTranslations { get; set; }
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options)
        : base(options)
        {
        }

    }
}
