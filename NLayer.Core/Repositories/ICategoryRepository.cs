namespace NLayer.Core.Repositories
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<List<Category>> GetAllCategoryWithProduct();

        Task<Category> GetCategoryWithProductById(int id);

    }
}
