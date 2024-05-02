using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NLayer.Core.DTOs.CategoryDTOs;
using NLayer.Core.DTOs.ProductDTOs;
using NLayer.Core.Model;
using NLayer.Core.Services;
using NLayer.WEB.Filters;
using NLayer.WEB.Models;
using NLayer.WEB.Services;

namespace NLayer.WEB.Controllers
{
    public class ProductsController : Controller
    {

        private readonly ProductApiService _productApiService;
        private readonly CategoryApiService _categoryApiService;
        public ProductsController(ProductApiService productApiService, CategoryApiService categoryApiService)
        {
            _productApiService = productApiService;
            _categoryApiService = categoryApiService;
        }

        public async Task<IActionResult> Products()
        {
            var products = await _productApiService.GetProductsWithCategoryAsync();

            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Save()
        {
            var categoriesDto = await _categoryApiService.GetAllAsync();

            ViewBag.categories = new SelectList(categoriesDto, "Id", "Name");

            return View();
        }

        // Filter yaz
        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                await _productApiService.SaveAsync(productDto);

                return RedirectToAction(nameof(Products));
            }

            var categories = await _categoryApiService.GetAllAsync();

            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            return View(productDto);
        }

        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var product = await _productApiService.GetByIdAsync(id);

            var categories = await _categoryApiService.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name", product.CategoryId);

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                await _productApiService.UpdateAsync(productDto);

                return RedirectToAction(nameof(Products));
            }

            var categories = await _categoryApiService.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name", productDto.CategoryId);

            return View(productDto);
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int id)
        {
            await _productApiService.RemoveAsync(id);

            return RedirectToAction(nameof(Products));
        }

    }
}
