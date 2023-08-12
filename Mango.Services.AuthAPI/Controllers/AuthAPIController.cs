using Mango.Services.AuthAPI.Models.Dto;
using Mango.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.AuthAPI.Controllers
{
	[Route("api/auth")]
	[ApiController]
	public class AuthAPIController : ControllerBase
	{

		private readonly IAuthService _authService;
		protected ResponseDto _response;
		public AuthAPIController(IAuthService authService)
		{
			_authService = authService;
			_response = new();
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegistrationDto registration)
		{
			var errorMessage = await _authService.Register(registration);
			if (!string.IsNullOrEmpty(errorMessage))
			{
				_response.IsSuccess = false;
				_response.Message = errorMessage;
				return BadRequest(_response);
			}
			return Ok(_response);
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginDto login)
		{
			var loginResponse = await _authService.Login(login);
			if (loginResponse.User == null)
			{
				_response.IsSuccess = false;
				_response.Message = "Username or password is incorrect";
				return BadRequest(_response);
			}
			_response.Result = loginResponse;
			return Ok(_response);
		}

		[HttpPost("assignRole")]
		public async Task<IActionResult> AssignRole([FromBody] RegistrationDto registration)
		{
			var assignRoleSuccess = await _authService.AssignRole(registration.Email, registration.Role.ToUpper());
			if (!assignRoleSuccess)
			{
				_response.IsSuccess = false;
				_response.Message = "Assign role failed";
				return BadRequest(_response);
			}
			return Ok(_response);
		}
	}
}
