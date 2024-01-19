using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;

namespace Mango.Services.IdentityServer
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			builder.Services.AddAuthentication(options =>
			{
				options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
			})
.AddCookie()
.AddOpenIdConnect(options =>
{
	options.Authority = builder.Configuration["IdentityServer:Authority"];
	options.ClientId = builder.Configuration["IdentityServer:ClientId"];
	options.ClientSecret = builder.Configuration["IdentityServer:ClientSecret"];
	options.CallbackPath = builder.Configuration["IdentityServer:CallbackPath"];
	options.ResponseType = "code";
	options.Scope.Add("openid");
	options.Scope.Add("profile");
	options.TokenValidationParameters = new TokenValidationParameters
	{
		NameClaimType = "name",
		RoleClaimType = "role"
	};
});
			// Add services to the container.
			builder.Services.AddControllersWithViews();



			var app = builder.Build();
			CreateHostBuilder(args).Build().Run();
			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
		public static IHostBuilder CreateHostBuilder(string[] args) =>
	   Host.CreateDefaultBuilder(args)
		   .ConfigureWebHostDefaults(webBuilder =>
		   {
			   webBuilder.UseStartup<Startup>();
		   });
	}
}
