using Mango.Services.CatalogAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.CatalogAPI.Data
{
	public class AppDbContext:DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
			
		}
		public DbSet<CatologItem> CatalogItems { get; set; }
	}
	
}
