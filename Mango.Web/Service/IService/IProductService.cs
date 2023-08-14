using Mango.Web.Models;

namespace Mango.Web.Service.IService
{
	public interface IProductService
	{
		Task<ResponseDto?> GetAllProductsAsync();
		Task<ResponseDto?> GetProductsByIdAsync(int id);

		Task<ResponseDto?> CreateProductsAsync(ProductDto product);

		Task<ResponseDto?> UpdateProductsAsync(ProductDto product);
		Task<ResponseDto?> DeleteProductsAsync(int id);
	}
}
