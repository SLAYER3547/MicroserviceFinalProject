using Mango.Services.IdentityServer.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace Mango.Services.IdentityServer.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
		
		[HttpPost]
		public async Task<IActionResult> Login(string username, string password)
		{
			// Burada, gerçek bir kimlik doðrulama iþlemi gerçekleþtirmeniz gerekir.
			// Bu örnekte basit bir þifre kontrolü yapýlýyor.

			if (IsValidUser(username, password))
			{
				// Kullanýcý doðrulandýysa, kullanýcý bilgilerini içeren bir ClaimsIdentity oluþturun.
				var claims = new[] { new Claim(ClaimTypes.Name, username) };
				var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

				// Kullanýcýyý oturum açýk olarak iþaretle ve kimlik bilgilerini taþýyan bir çerez oluþturun.
				var principal = new ClaimsPrincipal(identity);
				await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

				// Giriþ baþarýlý, ana sayfaya yönlendirin veya baþka bir sayfaya yönlendirin.
				return RedirectToAction("Index", "Home");
			}

			// Kullanýcý doðrulanamadý, hata mesajýný gösterin veya baþka bir iþlem yapýn.
			ModelState.AddModelError(string.Empty, "Geçersiz kullanýcý adý veya þifre");
			return View("Index");
		}

		public async Task<IActionResult> Logout()
		{
			// Kullanýcýyý oturumdan çýkarýn.
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

			// Çýkýþ yaptýktan sonra ana sayfaya yönlendirin veya baþka bir sayfaya yönlendirin.
			return RedirectToAction("Index", "Home");
		}

		private bool IsValidUser(string username, string password)
		{
			// Bu bölümü kendi kimlik doðrulama mantýðýnýza göre doldurun.
			// Örneðin, veritabanýnda kullanýcý adý ve þifre kontrolü yapabilirsiniz.

			// Bu örnekte sadece basit bir kontrol var.
			return username == "demo" && password == "demo";
		}
	}
}
