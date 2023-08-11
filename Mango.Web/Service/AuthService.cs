using Mango.Web.Models;
using Mango.Web.Service.IService;
using Mango.Web.Utility;

namespace Mango.Web.Service
{
    public class AuthService : IAuthService
    {
        private readonly IBaseService _baseService;
        public AuthService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<ResponseDto?> AssignRoleAsync(RegistrationDto registration)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                APIType = SD.ApiType.POST,
                Data = registration,
                Url = SD.AuthAPIBase + "/api/auth/assignRole"
            });
        }

        public async Task<ResponseDto?> LoginAsync(LoginDto login)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                APIType = SD.ApiType.POST,
                Data = login,
                Url = SD.AuthAPIBase + "/api/auth/login"
            });
        }

        public async Task<ResponseDto?> RegisterAsync(RegistrationDto registration)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                APIType = SD.ApiType.POST,
                Data = registration,
                Url = SD.AuthAPIBase + "/api/auth/register"
            });
        }
    }
}
