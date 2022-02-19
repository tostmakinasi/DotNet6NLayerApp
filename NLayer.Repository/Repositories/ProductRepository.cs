using Microsoft.EntityFrameworkCore;
using NLayer.Core;
using NLayer.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Product>> GetAllProductsWithCategory()
        {
            return await _context.Products.Include(x=> x.Category).ToListAsync();
        }

        //public async Task<List<Product>> GetProductsWithCategoryByCategoryId(int categoryId)
        //{
        //    return await _context.Products.Where(x=> x.CategoryId == categoryId).Include(x=> x.Category).ToListAsync();
        //}

        public async Task<Product> GetProductsWithCategoryById(int id)
        {
            return await _context.Products.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);
        }

    }
}
