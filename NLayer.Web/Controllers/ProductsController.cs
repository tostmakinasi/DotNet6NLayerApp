using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NLayer.Core;
using NLayer.Core.DTOs;
using NLayer.Core.Services;
using NLayer.Web.Services;

namespace NLayer.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductApiService _productService;
        private readonly CategoryApiService _categoryService;

        public ProductsController(CategoryApiService categoryService, ProductApiService productApiService)
        {
            _categoryService = categoryService;
            _productService = productApiService;
        }

        public async Task<IActionResult> Index()
        {
            return View((await _productService.GetAllProductsWithCategoryAsync()));
        }

        public async Task<IActionResult> Save()
        {
            var categories = await _categoryService.GetAllAsync();

            ViewBag.categories = new SelectList(categories, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                await _productService.SaveAsync(productDto);

                return RedirectToAction(nameof(Index));
            }

            var categories = await _categoryService.GetAllAsync();

            ViewBag.categories = new SelectList(categories, "Id", "Name");

            return View();
        }
        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        public async Task<IActionResult> Update(int id)
        {
            var product = await _productService.GetByIdAsync(id);

            var categories = await _categoryService.GetAllAsync();

            ViewBag.categories = new SelectList(categories, "Id", "Name",product.CategoryId);

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                await _productService.UpdateAsync(productDto);

                return RedirectToAction(nameof(Index));
            }

            var categories = await _categoryService.GetAllAsync();

            ViewBag.categories = new SelectList(categories, "Id", "Name", productDto.CategoryId);

            return View(productDto);
        }

        public async Task<IActionResult> Remove(int id)
        {
            await _productService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
