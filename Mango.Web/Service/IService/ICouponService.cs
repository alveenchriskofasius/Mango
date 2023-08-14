using Mango.Web.Models;

namespace Mango.Web.Service.IService
{
	public interface ICouponService
	{
		Task<ResponseDto?> GetCouponsAsync(string couponId);
		Task<ResponseDto?> GetAllCouponsAsync();
		Task<ResponseDto?> GetCouponsByIdAsync(int id);

		Task<ResponseDto?> CreateCouponsAsync(CouponDto coupon);

		Task<ResponseDto?> UpdateCouponsAsync(CouponDto coupon);
		Task<ResponseDto?> DeleteCouponsAsync(int id);
	}
}
