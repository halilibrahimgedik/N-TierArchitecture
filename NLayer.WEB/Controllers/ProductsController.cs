using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.Services;
using NLayer.WEB.Models;

namespace NLayer.WEB.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Products()
        {
            var customResponseDto = await _productService.GetProductsWithCategoryAsync();
            return View(customResponseDto.Data);
        }
    }
}
