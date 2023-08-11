using Mango.Services.AuthAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.AuthAPI.Data
{
	public class AppDbContext : IdentityDbContext<User>
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{


		}
		public DbSet<User> users { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);


		}

	}
}
