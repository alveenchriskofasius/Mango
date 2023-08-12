using Mango.Services.AuthAPI.Data;
using Mango.Services.AuthAPI.Models;
using Mango.Services.AuthAPI.Models.Dto;
using Mango.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Identity;

namespace Mango.Services.AuthAPI.Service
{
	public class AuthService : IAuthService
	{
		private readonly AppDbContext _db;
		private readonly UserManager<User> _userManager;
		private readonly IJwtTokenGenerator _jwtTokenGenerator;
		private readonly RoleManager<IdentityRole> _roleManager;

		public AuthService(AppDbContext db, IJwtTokenGenerator jwtTokenGenerator, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
		{
			_db = db;
			_jwtTokenGenerator = jwtTokenGenerator;
			_userManager = userManager;
			_roleManager = roleManager;
		}

		public async Task<bool> AssignRole(string email, string roleName)
		{
			var user = _db.Users.FirstOrDefault(x => x.Email.ToLower().Equals(email.ToLower()));
			if (user != null)
			{
				if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
				{
					// create role if not exists
					_roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
				}
				await _userManager.AddToRoleAsync(user, roleName);
				return true;
			}
			return false;
		}

		public async Task<LoginResponseDto> Login(LoginDto loginDto)
		{
			var user = _db.Users.FirstOrDefault(x => x.UserName.ToLower().Equals(loginDto.Username.ToLower()));
			bool isValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
			if (user == null || isValid == false)
			{
				return new LoginResponseDto() { User = null, Token = "" };
			}
			var token = _jwtTokenGenerator.GenerateToken(user);
			UserDto userDto = new()
			{
				Email = user.Email,
				ID = user.Id,
				Name = user.Name,
				PhoneNumber = user.PhoneNumber
			};

			LoginResponseDto loginResponseDto = new LoginResponseDto()
			{
				User = userDto,
				Token = token
			};
			return loginResponseDto;
		}

		public async Task<string> Register(RegistrationDto registrationDto)
		{
			User user = new()
			{
				UserName = registrationDto.Email,
				Email = registrationDto.Email,
				NormalizedEmail = registrationDto.Email.ToUpper(),
				Name = registrationDto.Name,
				PhoneNumber = registrationDto.PhoneNumber

			};

			try
			{
				var result = await _userManager.CreateAsync(user, registrationDto.Password);
				if (result.Succeeded)
				{
					var userToReturn = _db.Users.First(x => x.UserName == registrationDto.Email);

					UserDto userDto = new()
					{
						Email = registrationDto.Email,
						ID = userToReturn.Id,
						Name = userToReturn.Name,
						PhoneNumber = userToReturn.PhoneNumber
					};

					return "";
				}
				else
				{
					return result.Errors.FirstOrDefault().Description;
				}
			}
			catch (Exception ex)
			{

			}

			return "Error Encountered";
		}
	}
}
