using NLayer.Core.DTOs;
using NLayer.Core.ViewModels;

namespace NLayer.Web.Services
{
    public class CategoryApiService
    {
        private readonly HttpClient _httpClient;

        public CategoryApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CategoryDto>> GetAllAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDto<List<CategoryDto>>>("categories");

            return response.Data;
        }

        public async Task<CategoryDto> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDto<CategoryDto>>($"categories/{id}");      

            return response.Data;
        }

        public async Task<List<CategoryWithProductDto>> GetAllWithProductsAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDto<List<CategoryWithProductDto>>>("categories/GetAllWithProductsAsync");

            return response.Data;
        }

        public async Task<CategoryWithProductDto> GetWithProductsByIdAsyn(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDto<CategoryWithProductDto>>($"categories/GetWithProductsById/{id}");

            return response.Data;
        }

        public async Task<CategoryDto> SaveAsync(CategoryDto categoryDto)
        {
            var response = await _httpClient.PostAsJsonAsync("categories",categoryDto);

            if (!response.IsSuccessStatusCode) return null;

            var responseBody = await response.Content.ReadFromJsonAsync<CustomResponseDto<CategoryDto>>();

            return responseBody.Data;
        }

        public async Task<List<CategoryDto>> SaveRangeAsync(List<CategoryDto > categoryDtos)
        {
            var response = await _httpClient.PostAsJsonAsync("categories/saverange", categoryDtos);

            if (!response.IsSuccessStatusCode) return null;

            var responseBody = await response.Content.ReadFromJsonAsync<CustomResponseDto<List<CategoryDto>>>();

            return responseBody.Data;
        }

        public async Task<bool> UpdateAsync(CategoryDto categoryDto)
        {
            var response = await _httpClient.PutAsJsonAsync("categories", categoryDto);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"categories/{id}");

            return response.IsSuccessStatusCode;
        }
    }
}
