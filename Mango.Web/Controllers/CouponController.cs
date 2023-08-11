using Mango.Web.Models;
using Mango.Web.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.Controllers
{
	public class CouponController : Controller
	{
		private readonly ICouponService _couponService;
		public CouponController(ICouponService couponService)
		{
			_couponService = couponService;
		}
		public async Task<IActionResult> Index()

		{
			List<CouponDto>? coupons = new();
			ResponseDto? response = await _couponService.GetAllCouponsAsync();
			if (response != null && response.IsSuccess)
			{
				coupons = JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(response.Result));
			}
			else { TempData["error"] = response?.Message; }
			return View(coupons);
		}

		public async Task<IActionResult> Delete(int id)
		{
			ResponseDto? response = await _couponService.GetCouponsByIdAsync(id);
			if (response != null && response.IsSuccess)
			{
				CouponDto? coupon = JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(response.Result));
				return View(coupon);
			}
			else { TempData["error"] = response?.Message; }
			return NotFound();
		}
		[HttpPost]
		public async Task<IActionResult> Delete(CouponDto coupon)
		{
			ResponseDto? response = await _couponService.DeleteCouponsAsync(coupon.ID);
			if (response != null && response.IsSuccess)
			{
				return RedirectToAction(nameof(Index));
			}
			else { TempData["error"] = response?.Message; }
			return View(coupon);
		}

		public async Task<IActionResult> Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(CouponDto coupon)
		{
			if (ModelState.IsValid)
			{
				ResponseDto? response = await _couponService.CreateCouponsAsync(coupon);
				if (response != null && response.IsSuccess) { TempData["success"] = "Coupon created successfully!"; return RedirectToAction(nameof(Index)); } else { TempData["error"] = response?.Message; }
			}
			return View(coupon);
		}
	}
}
