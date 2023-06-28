using NLayer.Core.DTOs;

namespace NLayer.Web.Services
{
    public class ProductApiService
    {
        private readonly HttpClient _httpClient;

        public ProductApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<List<ProductWithCategoryDto>> GetAllProductsWithCategoryAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDto<List<ProductWithCategoryDto>>>("GetAllProductsWithCategory");

            return response.Data;

        }

        public async Task<List<ProductDto>> GetAllAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDto<List<ProductDto>>>("products");

            return response.Data;
        }

        public async Task<ProductWithCategoryDto> GetProductWithCategoryAsync(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDto<ProductWithCategoryDto>>($"products/GetProductsWithCategory/{id}");

            return response.Data;
        }

        public async Task<ProductDto> SaveAsync(ProductDto productDto)
        {
            var response = await _httpClient.PostAsJsonAsync("products", productDto);

            if (!response.IsSuccessStatusCode) return null;

            var responseBody = await response.Content.ReadFromJsonAsync<CustomResponseDto<ProductDto>>();

            return responseBody.Data;
        }

        public async Task<List<ProductDto>> SaveRangeAsync(IEnumerable<ProductDto> productDtos)
        {
            var response = await _httpClient.PostAsJsonAsync("products/SaveRange", productDtos);

            if (!response.IsSuccessStatusCode) return null;
            var responseBody = await response.Content.ReadFromJsonAsync < CustomResponseDto<List<ProductDto>>>();

            return responseBody.Data;
        }

        public async Task<ProductDto> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDto<ProductDto>>($"products/{id}");

            return response.Data;
        }

        public async Task<bool> UpdateAsync(ProductDto productDto)
        {
            var response = await _httpClient.PutAsJsonAsync("products", productDto);

            return response.IsSuccessStatusCode;

        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"products/{id}");

            return response.IsSuccessStatusCode;

        }
    }
}
