using Mango.Web.Models;
using Mango.Web.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.Controllers
{
	public class ProductController : Controller
	{
		private readonly IProductService _productService;
		public ProductController(IProductService productService)
		{
			_productService = productService;
		}
		public async Task<IActionResult> Index()

		{
			List<ProductDto>? products = new();
			ResponseDto? response = await _productService.GetAllProductsAsync();
			if (response != null && response.IsSuccess)
			{
				products = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
			}
			else { TempData["error"] = response?.Message; }
			return View(products);
		}

		public async Task<IActionResult> Delete(int id)
		{
			ResponseDto? response = await _productService.GetProductsByIdAsync(id);
			if (response != null && response.IsSuccess)
			{
				ProductDto? product = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
				return View(product);
			}
			else { TempData["error"] = response?.Message; }
			return NotFound();
		}
		[HttpPost]
		public async Task<IActionResult> Delete(ProductDto product)
		{
			ResponseDto? response = await _productService.DeleteProductsAsync(product.ID);
			if (response != null && response.IsSuccess)
			{
				return RedirectToAction(nameof(Index));
			}
			else { TempData["error"] = response?.Message; }
			return View(product);
		}

		public async Task<IActionResult> Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(ProductDto product)
		{
			if (ModelState.IsValid)
			{
				ResponseDto? response = await _productService.CreateProductsAsync(product);
				if (response != null && response.IsSuccess) { TempData["success"] = "Product created successfully!"; return RedirectToAction(nameof(Index)); } else { TempData["error"] = response?.Message; }
			}
			return View(product);
		}
		public async Task<IActionResult> Edit(int id)
		{
			ResponseDto? response = await _productService.GetProductsByIdAsync(id);

			if (response != null && response.IsSuccess)
			{
				ProductDto? model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
				return View(model);
			}
			else
			{
				TempData["error"] = response?.Message;
			}
			return NotFound();
		}
		[HttpPost]
		public async Task<IActionResult> Edit(ProductDto product)
		{
			if (ModelState.IsValid)
			{
				ResponseDto? response = await _productService.UpdateProductsAsync(product);
				if (response != null && response.IsSuccess) { TempData["success"] = "Product updated successfully!"; return RedirectToAction(nameof(Index)); } else { TempData["error"] = response?.Message; }
			}
			return View(product);
		}
	}
}
