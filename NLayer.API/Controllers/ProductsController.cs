using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NLayer.API.Filters;
using NLayer.Core;
using NLayer.Core.DTOs;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{

    public class ProductsController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IProductService _service;

        public ProductsController(IMapper mapper, IProductService service)
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _service.GetAllAsync();
            var productsDtos = _mapper.Map<List<ProductDto>>(products.ToList());

            //return Ok(CustomResponseDto<List<ProductDto>>.Success(200,productsDtos));
            return CreateActionResult<List<ProductDto>>(CustomResponseDto<List<ProductDto>>.Success(200, productsDtos));
        }

        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _service.GetByIdAsync(id);

            var productDto = _mapper.Map<ProductDto>(product);

            return CreateActionResult<ProductDto>(CustomResponseDto<ProductDto>.Success(200, productDto));
        }

        [HttpGet("/[action]")]
        public async Task<IActionResult> GetAllProductsWithCategory()
        {
            var products = await _service.GetAllProductsWithCategory();

            return CreateActionResult(products);
        }

        [HttpGet("/[action]/{id}")]
        public async Task<IActionResult> GetProductsWithCategory(int id)
        {
            var product = await _service.GetProductsWithCategoryById(id);

            return CreateActionResult(product);
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto)
        {
            var product = await _service.AddAsync(_mapper.Map<Product>(productDto));
            var newProductDto = _mapper.Map<ProductDto>(product);

            return CreateActionResult<ProductDto>(CustomResponseDto<ProductDto>.Success(201, newProductDto));
        }

        [HttpPost("SaveRange")]
        public async Task<IActionResult> SaveRange(IEnumerable<ProductDto> productDtos)
        {
            var products = await _service.AddRangeAsync(_mapper.Map<List<Product>>(productDtos));
            var newProductsDtos = _mapper.Map<List<ProductDto>>(products);

            return CreateActionResult<List<ProductDto>>(CustomResponseDto<List<ProductDto>>.Success(201, newProductsDtos));
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateDto productDto)
        {
            await _service.UpdateAsync(_mapper.Map<Product>(productDto));

            return CreateActionResult<NoContentDto>(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _service.GetByIdAsync(id);
            await _service.RemoveAsync(product);

            return CreateActionResult<NoContentDto>(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}
