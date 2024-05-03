using NLayer.Core.DTOs.ResponseDTOs;
using NLayer.Core.Model;
using System.Linq.Expressions;

namespace NLayer.Core.Services
{
    // Controller'larımıza istenilen Datayı dönen (Dto) Generic Service interface'imiz 
    public interface IServiceWithDto<Entity,Dto> where Entity : BaseEntity where Dto : class
    {
        Task<CustomResponseDto<Dto>> GetByIdAsync(int id);
        Task<CustomResponseDto<IEnumerable<Dto>>> GetAllAsync();

        Task<CustomResponseDto<IEnumerable<Dto>>> Where(Expression<Func<Entity, bool>> expression);
        Task<CustomResponseDto<bool>> AnyAsync(Expression<Func<Entity, bool>> expression);

        Task<CustomResponseDto<Dto>> AddAsync(Dto dto);
        Task<CustomResponseDto<IEnumerable<Dto>>> AddRangeAsync(IEnumerable<Dto> dtos);

        Task<CustomResponseDto<NoResponseDto>> UpdateAsync(Dto dto);
        Task<CustomResponseDto<NoResponseDto>> RemoveAsync(int id);
        Task<CustomResponseDto<NoResponseDto>> RemoveRangeAsync(IEnumerable<int> ids); // IEnumerable istediğimiz liste tipine cast edebileceğimizden dolayı IEnumerable'ı kullandık (List<int>'a , int[]'ye, HashSet<>'e cast edebiliriz mesela)
    }
}
