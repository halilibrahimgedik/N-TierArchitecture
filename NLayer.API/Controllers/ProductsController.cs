using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.API.Filters;
using NLayer.Core.DTOs.ProductDTOs;
using NLayer.Core.DTOs.ResponseDTOs;
using NLayer.Core.Model;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{
    public class ProductsController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IProductService _productService;

        public ProductsController(IMapper mapper,IProductService productService)
        {
            _mapper = mapper;
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllAsync();
            var productDtos = _mapper.Map<List<ProductDto>>(products.ToList());

            return CreateActionResult(CustomResponseDto<List<ProductDto>>.Success(200, productDtos));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductsWithCategory()
        {
            var products = await _productService.GetProductsWithCategoryAsync();
            return CreateActionResult(products);
        }

        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            var productDto = _mapper.Map<ProductDto>(product);

            return CreateActionResult(CustomResponseDto<ProductDto>.Success(200, productDto));
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            product.CreatedDate = DateTime.Now;
            var productData = await _productService.AddAsync(product);
            var ProductDtoData = _mapper.Map<ProductDto>(product);

            return CreateActionResult(CustomResponseDto<ProductDto>.Success(201, ProductDtoData));
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateDto productUpdateDto)
        {
            var product = _mapper.Map<Product>(productUpdateDto);
            await _productService.UpdateAsync(product); // geriye data dönmüyor

            return CreateActionResult(CustomResponseDto<NoResponseDto>.Success(204)); // 204 işlem başarılı oldu kodu
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            await _productService.RemoveAsync(product);

            return CreateActionResult(CustomResponseDto<NoResponseDto>.Success(204));
        }
    }
}
