using Mango.Web.Models;
using Mango.Web.Service.IService;
using Mango.Web.Utility;

namespace Mango.Web.Service
{
	public class ProductService : IProductService
	{
		private readonly IBaseService _baseService;
		public ProductService(IBaseService baseService)
		{
			_baseService = baseService;
		}

		public async Task<ResponseDto?> CreateProductsAsync(ProductDto product)
		{
			return await _baseService.SendAsync(new RequestDto()
			{
				APIType = SD.ApiType.POST,
				Data = product,
				Url = SD.ProductAPIBase + "/api/product"
			});
		}

		public async Task<ResponseDto?> DeleteProductsAsync(int id)
		{
			return await _baseService.SendAsync(new RequestDto()
			{
				APIType = SD.ApiType.DELETE,
				Url = SD.ProductAPIBase + "/api/product/" + id
			});
		}

		public async Task<ResponseDto?> GetAllProductsAsync()
		{
			return await _baseService.SendAsync(new RequestDto()
			{
				APIType = SD.ApiType.GET,
				Url = SD.ProductAPIBase + "/api/product"
			});
		}


		public async Task<ResponseDto?> GetProductsByIdAsync(int id)
		{
			return await _baseService.SendAsync(new RequestDto()
			{
				APIType = SD.ApiType.GET,
				Url = SD.ProductAPIBase + "/api/product/" + id
			});
		}

		public async Task<ResponseDto?> UpdateProductsAsync(ProductDto product)
		{
			return await _baseService.SendAsync(new RequestDto()
			{
				APIType = SD.ApiType.PUT,
				Data = product,
				Url = SD.ProductAPIBase + "/api/product"
			});
		}
	}
}
