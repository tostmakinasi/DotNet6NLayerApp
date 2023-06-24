using NLayer.Core.DTOs;

namespace NLayer.Core.Services
{
    public interface ICategoryService : IService<Category>
    {
        Task<CustomResponseDto<List<CategoryWithProductDto>>> GetAllCategoryWithProduct();

        Task<CustomResponseDto<CategoryWithProductDto>> GetCategoryWithProductById(int id);
    }
}
