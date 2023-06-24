namespace NLayer.Core.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<List<Product>> GetAllProductsWithCategory();

        //Task<List<Product>> GetProductsWithCategoryByCategoryId(int categoryId);

        Task<Product> GetProductsWithCategoryById(int id);
    }
}
