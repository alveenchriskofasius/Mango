using Mango.Web.Models;
using Mango.Web.Service.IService;
using Mango.Web.Utility;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using NuGet.Protocol;
using NuGet.Protocol.Plugins;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Mango.Web.Controllers
{
	public class AuthController : Controller
	{
		private readonly IAuthService _authService;

		private readonly ITokenProvider _tokenProvider;
		public AuthController(IAuthService authService, ITokenProvider tokenProvider)
		{
			_authService = authService;
			_tokenProvider = tokenProvider;
		}

		[HttpGet]
		public IActionResult Login()
		{
			LoginDto login = new();
			return View(login);
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginDto login)
		{
			ResponseDto response = await _authService.LoginAsync(login);
			if (response != null && response.IsSuccess)
			{
				LoginResponseDto loginResponse = JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(response.Result));
				await SignInUser(loginResponse);
				_tokenProvider.SetToken(loginResponse.Token);
				return RedirectToAction("Index", "Home");
			}
			else { TempData["error"] = response?.Message; return View(login); }

		}
		[HttpGet]
		public IActionResult Register()
		{
			var roleList = new List<SelectListItem>() {
				new SelectListItem { Text = SD.RoleAdmin, Value = SD.RoleAdmin},new SelectListItem { Text = SD.RoleCustomer, Value = SD.RoleCustomer }
				};
			ViewBag.RoleList = roleList;
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegistrationDto registration)
		{
			ResponseDto response = await _authService.RegisterAsync(registration);
			ResponseDto assignRole;
			if (response != null && response.IsSuccess)
			{
				if (string.IsNullOrEmpty(registration.Role))
				{
					registration.Role = SD.RoleCustomer;
				}
				assignRole = await _authService.AssignRoleAsync(registration);
				if (assignRole != null && assignRole.IsSuccess)
				{
					TempData["success"] = "Registration Successfully";
					return RedirectToAction(nameof(Login));
				}
			}
			else { TempData["error"] = response?.Message; }

			var roleList = new List<SelectListItem>() {
				new SelectListItem { Text = SD.RoleAdmin, Value = SD.RoleAdmin},new SelectListItem { Text = SD.RoleCustomer, Value = SD.RoleCustomer }
				};
			ViewBag.RoleList = roleList;
			return View(registration);
		}

		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync();
			_tokenProvider.ClearToken();
			return RedirectToAction("Index", "Home");
		}

		private async Task SignInUser(LoginResponseDto login)
		{
			var handler = new JwtSecurityTokenHandler();
			var jwt = handler.ReadJwtToken(login.Token);

			var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
			identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email,
				jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));
			identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub,
				jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub).Value));
			identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name,
				jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name).Value));


			identity.AddClaim(new Claim(ClaimTypes.Name,
				jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));
			identity.AddClaim(new Claim(ClaimTypes.Role,
				jwt.Claims.FirstOrDefault(u => u.Type == "role").Value));

			var principal = new ClaimsPrincipal(identity);
			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
		}
	}
}
