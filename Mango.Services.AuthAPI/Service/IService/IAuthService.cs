using Mango.Services.AuthAPI.Models.Dto;

namespace Mango.Services.AuthAPI.Service.IService
{
	public interface IAuthService
	{
		Task<string> Register(RegistrationDto registrationDto);
		Task<LoginResponseDto> Login(LoginDto loginDto);
	}
}
