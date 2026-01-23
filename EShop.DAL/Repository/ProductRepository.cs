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
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _Context;

        public ProductRepository(ApplicationDbContext context )
        {
            _Context = context;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _Context.Products.Include(c => c.Translations).Include(c => c.User).ToListAsync();
        }
        public async Task<Product> AddAsync(Product request)
        {
           await _Context.Products.AddAsync( request );
            await _Context.SaveChangesAsync();
            return request;
        }
    }
}
