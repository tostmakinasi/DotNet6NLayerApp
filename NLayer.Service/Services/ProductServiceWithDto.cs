using AutoMapper;
using Microsoft.AspNetCore.Http;
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
    public class ProductServiceWithDto : ServiceWithDto<Product, ProductDto>,IProductServiceWithDto
    {
        private readonly IProductRepository _productRepository;
        public ProductServiceWithDto(IGenericRepository<Product> repository,IProductRepository productRepository, IUnitOfWork unitOfWork, IMapper mapper) : base(repository, unitOfWork, mapper)
        {
            _productRepository = productRepository;
        }

        public async Task<CustomResponseDto<ProductDto>> AddAsync(ProductCreateDto dto)
        {
            var entity = _mapper.Map<Product>(dto);
            await _productRepository.AddAsync(entity);
            await _unitOfWork.CommitChangesAsync();

            var newDto = _mapper.Map<ProductDto>(entity);

            return CustomResponseDto<ProductDto>.Success(StatusCodes.Status201Created, newDto);
        }

        public async Task<CustomResponseDto<IEnumerable<ProductDto>>> AddRangeAsync(IEnumerable<ProductCreateDto> dtos)
        {
            var entities = _mapper.Map<IEnumerable<Product>>(dtos);
            await _productRepository.AddRangeAsync(entities);
            await _unitOfWork.CommitChangesAsync();

            var newDtos = _mapper.Map<IEnumerable<ProductDto>>(entities);

            return CustomResponseDto<IEnumerable<ProductDto>>.Success(StatusCodes.Status201Created, newDtos);
        }

        public async Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetAllProductsWithCategory()
        {
            var products = await _productRepository.GetAllProductsWithCategory();

            var productsDto = _mapper.Map<List<ProductWithCategoryDto>>(products);

            return CustomResponseDto<List<ProductWithCategoryDto>>.Success(200, productsDto);
        }

        public async Task<CustomResponseDto<ProductWithCategoryDto>> GetProductsWithCategoryById(int id)
        {
            var products = await _productRepository.GetProductsWithCategoryById(id);

            var productsDto = _mapper.Map<ProductWithCategoryDto>(products);

            return CustomResponseDto<ProductWithCategoryDto>.Success(200, productsDto);
        }

        public async Task<CustomResponseDto<NoContentDto>> UpdateAsync(ProductUpdateDto dto)
        {
            var entity = _mapper.Map<Product>(dto);
            _productRepository.Update(entity);
            await _unitOfWork.CommitChangesAsync();
            return CustomResponseDto<NoContentDto>.Success(StatusCodes.Status204NoContent);
        }
    }
}
