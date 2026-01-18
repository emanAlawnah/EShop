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

        public async Task <Category> CreateAsync(Category Request)
        {
           await _Context.AddAsync(Request);
           await _Context.SaveChangesAsync();
            return Request;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _Context.Categories.Include(c => c.Translations).Include(c=>c.User).ToListAsync();
        }

        public async Task<Category?> FindByIdAsync(int id)
        {
            return await _Context.Categories.Include(C=>C.Translations).FirstOrDefaultAsync(c=>c.Id == id);
        }

        public async Task DeleteAsync(Category category)
        {
         _Context.Categories.Remove(category);
          await _Context.SaveChangesAsync();

        }

        public async Task<Category?> UpdateAsync( Category category)
        {
            _Context.Categories.Update(category);
            await _Context.SaveChangesAsync();
            return category;
        }
    }
}
