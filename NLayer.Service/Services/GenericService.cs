using Microsoft.EntityFrameworkCore;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Service.Exceptions;
using System.Linq.Expressions;

namespace NLayer.Service.Services
{
    public class GenericService<T> : IGenericService<T> where T : class
    {
        private readonly IGenericRepository<T> _genericRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GenericService(IGenericRepository<T> genericRepository, IUnitOfWork unitOfWork)
        {
            _genericRepository = genericRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _genericRepository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await _genericRepository.AddRangeAsync(entities);
            await _unitOfWork.CommitAsync();
            return entities;
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _genericRepository.AnyAsync(expression);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _genericRepository.GetAll().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            if(id == 0)
            {
                throw new ClientSideException("bad request");
            }

            var isThereAny = await _genericRepository.GetByIdAsync((int)id);

            if (isThereAny == null)
            {
                throw new NotFoundException($"{typeof(T).Name} ({id}) not found");
            }

            return isThereAny;
        }

        public async Task RemoveAsync(T entity)
        {
            _genericRepository.Remove(entity); // IGenericRepository'de bulunan Remove metodu asenkron değil unutma
            await _unitOfWork.CommitAsync();
        }

        public async Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            _genericRepository.RemoveRange(entities);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _genericRepository.Update(entity);
            await _unitOfWork.CommitAsync();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _genericRepository.Where(expression);
        }
    }
}
