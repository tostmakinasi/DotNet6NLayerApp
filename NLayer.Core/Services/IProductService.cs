using NLayer.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Services
{
    public interface IProductService:IService<Product>
    {
        Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetAllProductsWithCategory();

        //Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategoryByCategoryId(int categoryId);

        Task<CustomResponseDto<ProductWithCategoryDto>> GetProductsWithCategoryById(int id);
    }
}
