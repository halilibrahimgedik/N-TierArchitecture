using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NLayer.Core.DTOs.ResponseDTOs;
using NLayer.Core.Model;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using System.Linq.Expressions;

namespace NLayer.Service.Services
{
    // Controller'larımıza istenilen Datayı dönen (Dto) Generic Service interface'imiz 
    public class ServiceWithDto<Entity, Dto> : IServiceWithDto<Entity, Dto> where Entity : BaseEntity where Dto : class
    {
        private readonly IGenericRepository<Entity> _genericRepository;
        protected readonly IUnitOfWork _unitOfWork; // protected olarak tasarla ki özeleştirilmiş EntityService sınıfılarında kullanabilelim.
        private readonly IMapper _mapper;

        public ServiceWithDto(IGenericRepository<Entity> genericRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        public async Task<CustomResponseDto<Dto>> AddAsync(Dto dto)
        {
            var newEntity = _mapper.Map<Entity>(dto);
            await _genericRepository.AddAsync(newEntity);
            await _unitOfWork.CommitAsync();

            var newDto = _mapper.Map<Dto>(newEntity);

            return CustomResponseDto<Dto>.Success(StatusCodes.Status201Created, newDto);
        }

        public async Task<CustomResponseDto<IEnumerable<Dto>>> AddRangeAsync(IEnumerable<Dto> dtos)
        {
            var newEntities = _mapper.Map<IEnumerable<Entity>>(dtos);
            await _genericRepository.AddRangeAsync(newEntities);
            await _unitOfWork.CommitAsync();

            var newDtos = _mapper.Map<IEnumerable<Dto>>(newEntities);

            return CustomResponseDto<IEnumerable<Dto>>.Success(StatusCodes.Status201Created, newDtos);
        }

        public async Task<CustomResponseDto<bool>> AnyAsync(Expression<Func<Entity, bool>> expression)
        {
            var result = await _genericRepository.AnyAsync(expression);

            return CustomResponseDto<bool>.Success(StatusCodes.Status200OK,result);
        }

        public async Task<CustomResponseDto<IEnumerable<Dto>>> GetAllAsync()
        {
            var entities = await _genericRepository.GetAll().ToListAsync();

            var dtos = _mapper.Map<IEnumerable<Dto>>(entities);

            return CustomResponseDto<IEnumerable<Dto>>.Success(StatusCodes.Status200OK,dtos);
        }

        public async Task<CustomResponseDto<Dto>> GetByIdAsync(int id)
        {
            var entity = await _genericRepository.GetByIdAsync(id);

            var dto = _mapper.Map<Dto>(entity);

            return CustomResponseDto<Dto>.Success(StatusCodes.Status200OK,dto);
        }

        public async Task<CustomResponseDto<NoResponseDto>> RemoveAsync(int id)
        {
            var entity = await _genericRepository.GetByIdAsync(id);

            var dto = _mapper.Map<Dto>(entity);
            await _unitOfWork.CommitAsync();

            return CustomResponseDto<NoResponseDto>.Success(StatusCodes.Status204NoContent);
        }

        public async Task<CustomResponseDto<NoResponseDto>> RemoveRangeAsync(IEnumerable<int> ids)
        {
            var entities = await _genericRepository.Where(x => ids.Contains(x.Id)).ToListAsync();
            _genericRepository.RemoveRange(entities);
            await _unitOfWork.CommitAsync();

            return CustomResponseDto<NoResponseDto>.Success(StatusCodes.Status204NoContent);
        }

        public async Task<CustomResponseDto<NoResponseDto>> UpdateAsync(Dto dto)
        {
            var entity = _mapper.Map<Entity>(dto);

            _genericRepository.Update(entity);
            await _unitOfWork.CommitAsync();

            return CustomResponseDto<NoResponseDto>.Success(StatusCodes.Status204NoContent);
        }

        public async Task<CustomResponseDto<IEnumerable<Dto>>> Where(Expression<Func<Entity, bool>> expression)
        {
            var entities = await _genericRepository.Where(expression).ToListAsync();

            var dtos = _mapper.Map<IEnumerable<Dto>>(entities);

            return CustomResponseDto<IEnumerable<Dto>>.Success(StatusCodes.Status200OK, dtos);
        }
    }
}
