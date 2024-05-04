using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs.ProductDTOs;
using NLayer.Core.Model;
using NLayer.WEB.Services;

namespace NLayer.WEB.Controllers
{
    public class ProductControllerWithGenericApiService : Controller
    {
        private readonly IGenericApiService<Product,ProductDto> _productService;
        private const string _enpoint = "https://localhost:7253/api/products";

        public ProductControllerWithGenericApiService(IGenericApiService<Product, ProductDto> productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllAsync(_enpoint);
            return View(products);
        }
    }
}
