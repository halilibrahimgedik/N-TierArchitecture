using AutoMapper;
using NLayer.Core.DTOs.CategoryDTOs;
using NLayer.Core.DTOs.ResponseDTOs;
using NLayer.Core.Model;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Services
{
    public class CategoryService : GenericService<Category>, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(IGenericRepository<Category> genericRepository, IUnitOfWork unitOfWork, ICategoryRepository categoryRepository, IMapper mapper) : base(genericRepository, unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<CategoryWithProductsDto>> GetCategoryByIdWithProductsAsync(int categoryId)
        {
            if(categoryId == 0)
            {
                // Exception fırlat;
            }
            
            var categoryWithProducts = await _categoryRepository.GetCategoryByIdWithProductsAsync(categoryId);

            if(categoryWithProducts == null)
            {
                // exception fırlat
            }

            var categoryWithProductsDto = _mapper.Map<CategoryWithProductsDto>(categoryWithProducts);

            return CustomResponseDto<CategoryWithProductsDto>.Success(200, categoryWithProductsDto);
        }
    }
}
