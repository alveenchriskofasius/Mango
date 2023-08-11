using Microsoft.AspNetCore.Identity;

namespace Mango.Services.AuthAPI.Models
{
	public class User : IdentityUser
	{
		public string Name { get; set; }
	}
}
