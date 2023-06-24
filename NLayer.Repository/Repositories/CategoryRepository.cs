using Microsoft.EntityFrameworkCore;
using NLayer.Core;
using NLayer.Core.Repositories;

namespace NLayer.Repository.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Category>> GetAllCategoryWithProduct()
        {
            return await _context.Categories.Include(x => x.Products).ToListAsync();
        }

        public async Task<Category> GetCategoryWithProductById(int id)
        {
            return await _context.Categories.Include(x => x.Products).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
