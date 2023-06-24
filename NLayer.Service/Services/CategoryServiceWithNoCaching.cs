using AutoMapper;
using NLayer.Core;
using NLayer.Core.DTOs;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;

namespace NLayer.Service.Services
{
    public class CategoryServiceWithNoCaching : Service<Category>, ICategoryService
    {
        private readonly ICategoryRepository _repository;

        private readonly IMapper _mapper;
        public CategoryServiceWithNoCaching(IUnitOfWork unitOfWork, ICategoryRepository repository, IMapper mapper) : base(unitOfWork, repository)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<List<CategoryWithProductDto>>> GetAllCategoryWithProduct()
        {
            var categories = await _repository.GetAllCategoryWithProduct();

            var categoriesDtos = _mapper.Map<List<CategoryWithProductDto>>(categories);

            return CustomResponseDto<List<CategoryWithProductDto>>.Success(200, categoriesDtos);
        }

        public async Task<CustomResponseDto<CategoryWithProductDto>> GetCategoryWithProductById(int id)
        {
            var category = await _repository.GetCategoryWithProductById(id);

            var categoryDto = _mapper.Map<CategoryWithProductDto>(category);

            return CustomResponseDto<CategoryWithProductDto>.Success(200, categoryDto);
        }
    }
}
