using AMLDotNetCore.BlazorServer.Models;
using Microsoft.EntityFrameworkCore;

namespace AMLDotNetCore.BlazorServer.Database
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}

		public DbSet<BlogModel> Blogs { get; set; }	
	}
}
