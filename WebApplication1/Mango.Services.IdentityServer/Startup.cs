using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

public class Startup
{
	public void ConfigureServices(IServiceCollection services)
	{
		services.AddIdentityServer()
			.AddInMemoryClients(GetClients())
			.AddInMemoryIdentityResources(GetIdentityResources())
			.AddInMemoryApiResources(GetApiResources())
			.AddTestUsers(GetUsers())
			.AddDeveloperSigningCredential(); // Bu satır sadece geliştirme ortamı içindir. Production ortamında gerçek bir sertifika kullanmalısınız.

		services.AddControllersWithViews();
	}

	public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
	{
		if (env.IsDevelopment())
		{
			app.UseDeveloperExceptionPage();
		//	app.UseShowPII(); // Bu satır sadece geliştirme ortamında PII'ı (Personally Identifiable Information) gösterir.
		}

		app.UseStaticFiles();

		app.UseRouting();

		app.UseIdentityServer();

		app.UseAuthorization();

		app.UseEndpoints(endpoints =>
		{
			endpoints.MapDefaultControllerRoute();
		});
	}

	private IEnumerable<Client> GetClients()
	{
		return new List<Client>
		{
			new Client
			{
				ClientId = "mvc-client",
				ClientSecrets = { new Secret("mvc-secret".Sha256()) },
				AllowedGrantTypes = GrantTypes.Code,
				RedirectUris = { "https://localhost:5002/signin-oidc" },
				PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },
				AllowedScopes = { IdentityServerConstants.StandardScopes.OpenId, IdentityServerConstants.StandardScopes.Profile }
			}
		};
	}

	private IEnumerable<IdentityResource> GetIdentityResources()
	{
		return new List<IdentityResource>
		{
			new IdentityResources.OpenId(),
			new IdentityResources.Profile()
		};
	}

	private IEnumerable<ApiResource> GetApiResources()
	{
		return new List<ApiResource>
		{
			new ApiResource("api1", "My API")
		};
	}

	private List<TestUser> GetUsers()
	{
		return new List<TestUser>
		{
			new TestUser
			{
				SubjectId = "1",
				Username = "alice",
				Password = "password",

				Claims = new List<Claim>
				{
					new Claim("name", "Alice"),
					new Claim("website", "https://alice.com")
				}
			},
			new TestUser
			{
				SubjectId = "2",
				Username = "bob",
				Password = "password",

				Claims = new List<Claim>
				{
					new Claim("name", "Bob"),
					new Claim("website", "https://bob.com")
				}
			}
		};
	}
}
