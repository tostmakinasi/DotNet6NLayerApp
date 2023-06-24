using NLayer.Core.DTOs;

namespace NLayer.Core.Services
{
    public interface IProductService : IService<Product>
    {
        Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetAllProductsWithCategory();

        //Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategoryByCategoryId(int categoryId);

        Task<CustomResponseDto<ProductWithCategoryDto>> GetProductsWithCategoryById(int id);
    }
}
