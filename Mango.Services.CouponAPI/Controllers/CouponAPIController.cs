using AutoMapper;
using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponAPI.Controllers
{
	[Route("api/coupon")]
	[ApiController]
	public class CouponAPIController : ControllerBase
	{
		private readonly AppDbContext _db;
		private ResponseDto _response;
		private IMapper _mapper;
		public CouponAPIController(AppDbContext db, IMapper mapper)
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
				IEnumerable<Coupon> coupons = _db.Coupons.ToList();
				_response.Result = _mapper.Map<IEnumerable<Coupon>>(coupons);
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
				Coupon coupon = _db.Coupons.First(x => x.ID == id);
				_response.Result = _mapper.Map<CouponDto>(coupon);

			}
			catch (Exception ex)
			{

				_response.IsSuccess = false; _response.Message = ex.Message;
			}
			return _response;
		}

		[HttpGet]
		[Route("GetByCode/{code}")]
		public ResponseDto GetByCode(string code)
		{
			try
			{
				Coupon coupon = _db.Coupons.First(x => x.Code.ToLower().Equals(code.ToLower()));
				_response.Result = _mapper.Map<CouponDto>(coupon);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false; _response.Message = ex.Message;
			}
			return _response;
		}

		[HttpPost]
		public ResponseDto Save([FromBody] CouponDto couponDto)
		{
			try
			{
				Coupon coupon = _mapper.Map<Coupon>(couponDto);
				_db.Coupons.Add(coupon);
				_db.SaveChanges();
				_response.Result = _mapper.Map<CouponDto>(coupon);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false; _response.Message = ex.Message;
			}
			return _response;
		}

		[HttpPut]
		public ResponseDto Update([FromBody] CouponDto couponDto)
		{
			try
			{
				Coupon coupon = _mapper.Map<Coupon>(couponDto);
				_db.Coupons.Update(coupon);
				_db.SaveChanges();
				_response.Result = _mapper.Map<CouponDto>(coupon);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false; _response.Message = ex.Message;
			}
			return _response;
		}

		[HttpDelete]
		[Route("{id:int}")]
		public ResponseDto Delete(int id)
		{
			try
			{
				Coupon coupon = _db.Coupons.First(x => x.ID == id);
				_db.Coupons.Remove(coupon);
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
