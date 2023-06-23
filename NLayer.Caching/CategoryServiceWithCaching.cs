using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using NLayer.Core;
using NLayer.Core.DTOs;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Caching
{
    public class CategoryServiceWithCaching : ICategoryService
    {
        private const string CacheCategoryKey = "categoryCache";
        private readonly ICategoryRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _memoryCache;
        private readonly IMapper _mapper;

        public CategoryServiceWithCaching(IMemoryCache memoryCache, IUnitOfWork unitOfWork, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _memoryCache = memoryCache;
            _unitOfWork = unitOfWork;
            _repository = categoryRepository;

            if (_memoryCache.TryGetValue(CacheCategoryKey, out _))
            {
                _memoryCache.Set<List<Category>>(CacheCategoryKey, _repository.GetAllCategoryWithProduct().Result);
            }
            _mapper = mapper;
        }

        public async Task<Category> AddAsync(Category entity)
        {
            await _repository.AddAsync(entity);
            await _unitOfWork.CommitChangesAsync();
            await CacheAllCategory();

            return entity;
        }

        public async Task<IEnumerable<Category>> AddRangeAsync(IEnumerable<Category> items)
        {
            await _repository.AddRangeAsync(items);
            await _unitOfWork.CommitChangesAsync();
            await CacheAllCategory();

            return items;
        }

        public Task<bool> AnyAsync(Expression<Func<Category, bool>> expression)
        {
            return Task.FromResult(_memoryCache.Get<List<Category>>(CacheCategoryKey).Any(expression.Compile()));
        }

        public Task<IEnumerable<Category>> GetAllAsync()
        {
            return Task.FromResult(_memoryCache.Get<IEnumerable<Category>>(CacheCategoryKey));
        }

        public Task<CustomResponseDto<List<CategoryWithProductDto>>> GetAllCategoryWithProduct()
        {
            var categories = _memoryCache.Get<List<Category>>(CacheCategoryKey);
            var categoryWithProductDto = _mapper.Map<List<CategoryWithProductDto>>(categories);

            return Task.FromResult(CustomResponseDto<List<CategoryWithProductDto>>.Success(200, categoryWithProductDto));
        }

        public Task<Category> GetByIdAsync(int id)
        {
            var category = _memoryCache.Get<List<Category>>(CacheCategoryKey).FirstOrDefault(x => x.Id == id);

            if(category == null)
            {
                throw new NotFoundException($"Category({id}) not found");
            }

            return Task.FromResult(category);
        }

        public Task<CustomResponseDto<CategoryWithProductDto>> GetCategoryWithProductById(int id)
        {
            var category = _memoryCache.Get<List<Category>>(CacheCategoryKey).FirstOrDefault(x => x.Id == id);

            if (category == null)
            {
                throw new NotFoundException($"Category({id}) not found");
            }
            var categoryWithProductDto = _mapper.Map<CategoryWithProductDto>(category);

            return Task.FromResult(CustomResponseDto<CategoryWithProductDto>.Success(200, categoryWithProductDto));
        }

        public async Task RemoveAsync(Category entity)
        {
            _repository.Remove(entity);
            await _unitOfWork.CommitChangesAsync();
            await CacheAllCategory();
        }

        public async Task RemoveRangeAsync(IEnumerable<Category> items)
        {
            _repository.RemoveRange(items);
            await _unitOfWork.CommitChangesAsync();
            await CacheAllCategory();
        }

        public async Task UpdateAsync(Category entity)
        {
            _repository.Update(entity);
            await _unitOfWork.CommitChangesAsync();
        }

        public IQueryable<Category> Where(Expression<Func<Category, bool>> expression)
        {
            return _memoryCache.Get<List<Category>>(CacheCategoryKey).Where(expression.Compile()).AsQueryable();
        }

        public async Task CacheAllCategory()
        {
            _memoryCache.Set<List<Category>>(CacheCategoryKey, await _repository.GetAllCategoryWithProduct());
        }
    }
}
