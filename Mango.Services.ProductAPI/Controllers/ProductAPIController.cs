using AutoMapper;
using Mango.Services.ProductAPI.Data;
using Mango.Services.ProductAPI.Models;
using Mango.Services.ProductAPI.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.ProductAPI.Controllers
{
	[Route("api/product")]
	[ApiController]
	public class ProductAPIController : ControllerBase
	{
		private readonly AppDbContext _db;
		private ResponseDto _response;
		private IMapper _mapper;
		public ProductAPIController(AppDbContext db, IMapper mapper)
		{
			_db = db;
			_mapper = mapper;
			_response = new ResponseDto();
		}

		[HttpGet]
		public ResponseDto Get()
		{
			try
			{
				IEnumerable<Product> Products = _db.Products.ToList();
				_response.Result = _mapper.Map<IEnumerable<Product>>(Products);
			}
			catch (Exception ex) { _response.IsSuccess = false; _response.Message = ex.Message; }
			return _response;
		}


		[HttpGet]
		[Route("{id:int}")]
		public ResponseDto Get(int id)
		{
			try
			{
				Product Product = _db.Products.First(x => x.ID == id);
				_response.Result = _mapper.Map<ProductDto>(Product);

			}
			catch (Exception ex)
			{

				_response.IsSuccess = false; _response.Message = ex.Message;
			}
			return _response;
		}



		[HttpPost]
		[Authorize(Roles = "ADMIN")]
		public ResponseDto Save([FromBody] ProductDto ProductDto)
		{
			try
			{
				Product Product = _mapper.Map<Product>(ProductDto);
				_db.Products.Add(Product);
				_db.SaveChanges();
				_response.Result = _mapper.Map<ProductDto>(Product);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false; _response.Message = ex.Message;
			}
			return _response;
		}

		[HttpPut]
		[Authorize(Roles = "ADMIN")]

		public ResponseDto Update([FromBody] ProductDto ProductDto)
		{
			try
			{
				Product Product = _mapper.Map<Product>(ProductDto);
				_db.Products.Update(Product);
				_db.SaveChanges();
				_response.Result = _mapper.Map<ProductDto>(Product);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false; _response.Message = ex.Message;
			}
			return _response;
		}

		[HttpDelete]
		[Route("{id:int}")]
		[Authorize(Roles = "ADMIN")]

		public ResponseDto Delete(int id)
		{
			try
			{
				Product Product = _db.Products.First(x => x.ID == id);
				_db.Products.Remove(Product);
				_db.SaveChanges();
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false; _response.Message = ex.Message;
			}
			return _response;
		}
	}
}
