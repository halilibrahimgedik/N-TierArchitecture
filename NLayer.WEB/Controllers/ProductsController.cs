using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NLayer.Core.DTOs.CategoryDTOs;
using NLayer.Core.DTOs.ProductDTOs;
using NLayer.Core.Model;
using NLayer.Core.Services;
using NLayer.WEB.Filters;
using NLayer.WEB.Models;

namespace NLayer.WEB.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public ProductsController(IProductService productService, IMapper mapper, ICategoryService categoryService)
        {
            _productService = productService;
            _mapper = mapper;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Products()
        {
            var customResponseDto = await _productService.GetProductsWithCategoryAsync();
            return View(customResponseDto.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Save()
        {
            var categories = await _categoryService.GetAllAsync();
            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());

            ViewBag.Categories = new SelectList(categoriesDto,"Id","Name");

            return View();
        }

        // Filter yaz
        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto)
        {
            if(ModelState.IsValid)
            {
                var product = _mapper.Map<Product>(productDto);
                await _productService.AddAsync(product);

                return RedirectToAction(nameof(Products));
            }

            var categories = await _categoryService.GetAllAsync();
            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());

            ViewBag.Categories = new SelectList(categoriesDto, "Id", "Name");

            return View(productDto);
        }

        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var product = await _productService.GetByIdAsync(id);

            var categories = await _categoryService.GetAllAsync();
            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());

            ViewBag.Categories = new SelectList(categoriesDto, "Id", "Name",product.CategoryId);

            return View(_mapper.Map<ProductDto>(product));
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductDto productDto)
        {
            if(ModelState.IsValid)
            {
                await _productService.UpdateAsync(_mapper.Map<Product>(productDto));

                return RedirectToAction(nameof(Products));
            }

            var categories = await _categoryService.GetAllAsync();
            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());

            ViewBag.Categories = new SelectList(categoriesDto, "Id", "Name", productDto.CategoryId);

            return View(productDto);
        }

        [ServiceFilter(typeof(NotFoundFilter<>))]
        [HttpPost]
        public async Task<IActionResult> Remove(int id)
        {
            var product = await _productService.GetByIdAsync(id);

            await _productService.RemoveAsync(_mapper.Map<Product>(product));

            return RedirectToAction(nameof(Products));
        }

    }
}
