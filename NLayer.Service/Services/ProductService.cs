using AutoMapper;
using NLayer.Core;
using NLayer.Core.DTOs;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Services
{
    public class ProductService : Service<Product>, IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        public ProductService(IUnitOfWork unitOfWork, IProductRepository repository, IMapper mapper) : base(unitOfWork, repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetAllProductsWithCategory()
        {
            var products = await _repository.GetAllProductsWithCategory();

            var productsDto = _mapper.Map<List<ProductWithCategoryDto>>(products);

            return CustomResponseDto<List<ProductWithCategoryDto>>.Success(200, productsDto);
        }

        //public async Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategoryByCategoryId(int categoryId)
        //{
        //    var products = await _repository.GetProductsWithCategoryByCategoryId(categoryId);

        //    var productsDto = _mapper.Map<List<ProductWithCategoryDto>>(products);

        //    return CustomResponseDto<List<ProductWithCategoryDto>>.Success(200, productsDto);
        //}

        public async Task<CustomResponseDto<ProductWithCategoryDto>> GetProductsWithCategoryById(int id)
        {
            var products = await _repository.GetProductsWithCategoryById(id);

            var productsDto = _mapper.Map<ProductWithCategoryDto>(products);

            return CustomResponseDto<ProductWithCategoryDto>.Success(200, productsDto);
        }
    }
}
