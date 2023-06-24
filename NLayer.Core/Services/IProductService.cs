using NLayer.Core.DTOs;

namespace NLayer.Core.Services
{
    public interface IProductService : IService<Product>
    {
        Task<List<ProductWithCategoryDto>> GetAllProductsWithCategory();

        //Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategoryByCategoryId(int categoryId);

        Task<ProductWithCategoryDto> GetProductsWithCategoryById(int id);
    }
}
