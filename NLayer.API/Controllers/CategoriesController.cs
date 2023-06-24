using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core;
using NLayer.Core.DTOs;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{

    public class CategoriesController : CustomBaseController
    {
        private readonly ICategoryService _service;
        private readonly IMapper _mapper;
        public CategoriesController(ICategoryService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _service.GetAllAsync();

            var categoriesDtos = _mapper.Map<List<CategoryDto>>(categories.ToList());

            return CreateActionResult<List<CategoryDto>>(CustomResponseDto<List<CategoryDto>>.Success(200, categoriesDtos));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _service.GetByIdAsync(id);

            var categoryDto = _mapper.Map<CategoryDto>(category);

            return CreateActionResult<CategoryDto>(CustomResponseDto<CategoryDto>.Success(200, categoryDto));
        }

        [HttpGet("/[action]")]
        public async Task<IActionResult> GetAllWithProducts()
        {
            var categories = await _service.GetAllCategoryWithProduct();

            return CreateActionResult(categories);
        }

        [HttpGet("/[action]/{id}")]
        public async Task<IActionResult> GetWithProductsById(int id)
        {
            var category = await _service.GetCategoryWithProductById(id);

            return CreateActionResult(category);
        }

        [HttpPost]
        public async Task<IActionResult> Save(CategoryDto categoryDto)
        {
            var category = await _service.AddAsync(_mapper.Map<Category>(categoryDto));

            var newCategoryDto = _mapper.Map<CategoryDto>(category);

            return CreateActionResult(CustomResponseDto<CategoryDto>.Success(201, newCategoryDto));
        }

        [HttpPost("/[action]")]
        public async Task<IActionResult> SaveRange(List<CategoryDto> categoryDtos)
        {
            var category = await _service.AddRangeAsync(_mapper.Map<List<Category>>(categoryDtos));

            var categoriesDtos = _mapper.Map<List<CategoryDto>>(categoryDtos);

            return CreateActionResult(CustomResponseDto<List<CategoryDto>>.Success(201, categoriesDtos));
        }

        [HttpPut]
        public async Task<IActionResult> Update(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);

            await _service.UpdateAsync(category);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _service.GetByIdAsync(id);
            await _service.RemoveAsync(category);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        //[HttpDelete("/[action]")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var category = await _service.GetByIdAsync(id);
        //    await _service.RemoveRangeAsync(category);

        //    return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        //}
    }
}
