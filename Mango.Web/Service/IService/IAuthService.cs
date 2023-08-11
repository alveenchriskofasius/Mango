using Mango.Web.Models;

namespace Mango.Web.Service.IService
{
	public interface IAuthService
	{
		Task<ResponseDto?> LoginAsync(LoginDto login);
		Task<ResponseDto?> RegisterAsync(RegistrationDto registration);
		Task<ResponseDto?> AssignRoleAsync(RegistrationDto registration);

	}
}
