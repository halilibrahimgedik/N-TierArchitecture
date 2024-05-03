using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.API.Filters;
using NLayer.Core.DTOs.ProductDTOs;
using NLayer.Core.DTOs.ResponseDTOs;
using NLayer.Core.Model;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{
    public class ProductControllerWithDto : CustomBaseController
    {
        private readonly IProductServiceWithDto _productServiceWitDto;

        public ProductControllerWithDto(IProductServiceWithDto productService)
        {
            _productServiceWitDto = productService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productServiceWitDto.GetAllAsync();

            return CreateActionResult(products);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductsWithCategory()
        {
            var products = await _productServiceWitDto.GetProductsWithCategoryAsync();

            return CreateActionResult(products);
        }

        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productServiceWitDto.GetByIdAsync(id);

            return CreateActionResult(product);
        }

        [HttpGet("Any/{id}")]
        public async Task<IActionResult> Any(int id)
        {
            var result = await _productServiceWitDto.AnyAsync(x => x.Id == id);
            return CreateActionResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateProductDto dto)
        {
            var newEntity = await _productServiceWitDto.AddAsync(dto);

            return CreateActionResult(newEntity);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddAll(List<CreateProductDto> dtos )
        {
            return CreateActionResult(await _productServiceWitDto.AddRangeAsync(dtos));
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateDto dto)
        { 
            return CreateActionResult(await _productServiceWitDto.UpdateAsync(dto));
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            return CreateActionResult(await _productServiceWitDto.RemoveAsync(id));
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> RemoveAll(List<int> ids)
        {
            return CreateActionResult(await _productServiceWitDto.RemoveRangeAsync(ids));
        }
    }
}
