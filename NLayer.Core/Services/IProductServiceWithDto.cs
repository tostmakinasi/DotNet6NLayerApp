using NLayer.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Services
{
    public interface IProductServiceWithDto:IServiceWithDto<Product, ProductDto>
    {
        Task<CustomResponseDto<ProductWithCategoryDto>> GetProductsWithCategoryById(int id);
        Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetAllProductsWithCategory();
        Task<CustomResponseDto<NoContentDto>> UpdateAsync(ProductUpdateDto dto);
        Task<CustomResponseDto<IEnumerable<ProductDto>>> AddRangeAsync(IEnumerable<ProductCreateDto> items);
        Task<CustomResponseDto<ProductDto>> AddAsync(ProductCreateDto entity);
    }
}
