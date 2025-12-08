using EShop.DAL.Data;
using EShop.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DAL.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AplicationDbContext _Context;

        public CategoryRepository(AplicationDbContext context)
        {
            _Context = context;
        }

        public Category Create(Category Request)
        {
            _Context.Categories.Add(Request);
            _Context.SaveChanges();
            return Request;
        }

        public List<Category> GetAll()
        {
            return _Context.Categories.Include(c => c.Translations).ToList();
        }
    }
}
