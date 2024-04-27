using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;
using NLayer.Core.DTOs.ResponseDTOs;
using NLayer.Core.Model;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IGenericService<Product> _genericService;

        public ProductsController(IMapper mapper, IGenericService<Product> genericService)
        {
            _mapper = mapper;
            _genericService = genericService;
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAll()
        //{
        //    var products = await _genericService.GetAllAsync();
        //    var productDtos = _mapper.Map<List<ProductDto>>(products.ToList());

        //    return Ok(productDtos);
        //}

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _genericService.GetAllAsync();
            var productDtos = _mapper.Map<List<ProductDto>>(products.ToList());

            return CreateActionResult(CustomResponseDto<List<ProductDto>>.Success(200, productDtos));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _genericService.GetByIdAsync(id);
            var productDto = _mapper.Map<ProductDto>(product);

            return CreateActionResult(CustomResponseDto<ProductDto>.Success(200, productDto));
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductDto productDto)
        {
            var product = await _genericService.AddAsync(_mapper.Map<Product>(productDto));
            var convertedProductDto = _mapper.Map<ProductDto>(product);

            return CreateActionResult(CustomResponseDto<ProductDto>.Success(201, convertedProductDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateDto productUpdateDto)
        {
            var product = _mapper.Map<Product>(productUpdateDto);
            await _genericService.UpdateAsync(product); // geriye data dönmüyor

            return CreateActionResult(CustomResponseDto<NoResponseDto>.Success(204)); // 204 işlem başarılı oldu kodu
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var product = await _genericService.GetByIdAsync(id);
            await _genericService.RemoveAsync(product);

            return CreateActionResult(CustomResponseDto<NoResponseDto>.Success(204));
        }
    }
}
