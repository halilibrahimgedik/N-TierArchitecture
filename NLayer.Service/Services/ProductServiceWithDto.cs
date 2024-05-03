using AutoMapper;
using Microsoft.AspNetCore.Http;
using NLayer.Core.DTOs.ProductDTOs;
using NLayer.Core.DTOs.ResponseDTOs;
using NLayer.Core.Model;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Services
{
    //Yeni ProductService Sınıfımız
    public class ProductServiceWithDto : ServiceWithDto<Product, ProductDto>, IProductServiceWithDto
    {
        private readonly IProductRepository _productRepository;

        public ProductServiceWithDto(IGenericRepository<Product> genericRepository, IMapper mapper, IUnitOfWork unitOfWork, IProductRepository productRepository) : base(genericRepository, mapper, unitOfWork)
        {
            _productRepository = productRepository;
        }


        // Overload
        public async Task<CustomResponseDto<ProductDto>> AddAsync(CreateProductDto dto)
        {
            var newEntity = _mapper.Map<Product>(dto);
            await _productRepository.AddAsync(newEntity);
            await _unitOfWork.CommitAsync();

            var newDto = _mapper.Map<ProductDto>(newEntity);

            return CustomResponseDto<ProductDto>.Success(StatusCodes.Status201Created,newDto);
        }

        public async Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategoryAsync()
        {
            var products = await _productRepository.GetProductsWithCategoryAsync();
            var productWithCategoryDtoList = _mapper.Map<List<ProductWithCategoryDto>>(products);

            return CustomResponseDto<List<ProductWithCategoryDto>>.Success(200, productWithCategoryDtoList);
        }
        
        // Overload
        public async Task<CustomResponseDto<NoResponseDto>> UpdateAsync(ProductUpdateDto dto)
        {
            var entity = _mapper.Map<Product>(dto);
            _productRepository.Update(entity);
            await _unitOfWork.CommitAsync();

            return CustomResponseDto<NoResponseDto>.Success(StatusCodes.Status204NoContent);
        }
    }
}
