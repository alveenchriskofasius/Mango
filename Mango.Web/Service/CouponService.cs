using Mango.Web.Models;
using Mango.Web.Service.IService;
using Mango.Web.Utility;

namespace Mango.Web.Service
{
    public class CouponService : ICouponService
    {
        private readonly IBaseService _baseService;
        public CouponService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> CreateCouponsAsync(CouponDto coupon)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                APIType = SD.ApiType.POST,
                Data = coupon,
                Url = SD.CouponAPIBase + "/api/coupon"
            });
        }

        public async Task<ResponseDto?> DeleteCouponsAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                APIType = SD.ApiType.DELETE,
                Url = SD.CouponAPIBase + "/api/coupon" + id
            });
        }

        public async Task<ResponseDto?> GetAllCouponsAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                APIType = SD.ApiType.GET,
                Url = SD.CouponAPIBase + "/api/coupon"
            });
        }

        public async Task<ResponseDto?> GetCouponsAsync(string couponId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                APIType = SD.ApiType.GET,
                Url = SD.CouponAPIBase + "/api/coupon/GetByCode/" + couponId
            });
        }

        public async Task<ResponseDto?> GetCouponsByIdAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                APIType = SD.ApiType.GET,
                Url = SD.CouponAPIBase + "/api/coupon" + id
            });
        }

        public async Task<ResponseDto?> UpdateCouponsAsync(CouponDto coupon)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                APIType = SD.ApiType.PUT,
                Data = coupon,
                Url = SD.CouponAPIBase + "/api/coupon"
            });
        }
    }
}
