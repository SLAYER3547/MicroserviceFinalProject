using MongoDB.Bson;
using MongoDB.Driver;
using Mango.Services.CatalogAPI.Data;
using Microsoft.EntityFrameworkCore;
using Mango.Services.CatalogAPI.Models;

namespace Mango.Services.CatalogAPI
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddDbContext<AppDbContext>(option =>
			{
				option.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
			});

			
			var dbClient = new MongoClient("mongodb://127.0.0.1:27017");
			var db = dbClient.GetDatabase("Catalog");
			var emp = db.GetCollection<CatalogMongo>("Type");

			var product = new CatalogMongo { ProductId = "2", Product = "Phone", Category = "Tecnology", Price = "15000", Description = "SmartPhone", StockStatus = true };

			await emp.InsertOneAsync(product);

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}
			
			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
