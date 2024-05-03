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

        [HttpPost]
        public async Task<IActionResult> Add(CreateProductDto dto)
        {
            var newEntity = await _productServiceWitDto.AddAsync(dto);

            return CreateActionResult(newEntity);
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
    }
}
