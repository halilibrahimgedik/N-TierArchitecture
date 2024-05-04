namespace NLayer.WEB.Services
{
    public interface IGenericApiService<T,TDto>
    {
        Task<List<TDto>> GetAllAsync(string endpoint);
        Task<TDto> GetByIdAsync(int id, string endpoint);
        Task<TDto> SaveAsync(TDto newEntity, string endpoint);
        Task<bool> UpdateAsync(TDto entity, string endpoint);
        Task<bool> RemoveAsync(int id, string endpoint);
    }
}
