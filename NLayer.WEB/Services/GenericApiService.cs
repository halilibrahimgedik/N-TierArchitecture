using NLayer.Core.DTOs.ResponseDTOs;
using NLayer.Core.Model;

namespace NLayer.WEB.Services
{
    public class GenericApiService<T,TDto> : IGenericApiService<T, TDto> where T : BaseEntity where TDto : class
    {
        private readonly HttpClient _httpClient;

        public GenericApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<TDto>> GetAllAsync(string endpoint)
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDto<List<TDto>>>(endpoint);
            return response.Data;
        }

        public async Task<TDto> GetByIdAsync(int id, string endpoint)
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDto<TDto>>($"{endpoint}/{id}");
            return response.Data;
        }

        public async Task<TDto> SaveAsync(TDto newEntity,string endpoint)
        {
            var response = await _httpClient.PostAsJsonAsync(endpoint, newEntity);

            if (!response.IsSuccessStatusCode)
            {
                // Handle error
                return default(TDto);
            }

            var responseBody = await response.Content.ReadFromJsonAsync<CustomResponseDto<TDto>>();
            return responseBody.Data;
        }

        public async Task<bool> UpdateAsync(TDto entity, string endpoint)
        {
            var response = await _httpClient.PutAsJsonAsync(endpoint, entity);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RemoveAsync(int id, string endpoint)
        {
            var response = await _httpClient.DeleteAsync($"{endpoint}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
