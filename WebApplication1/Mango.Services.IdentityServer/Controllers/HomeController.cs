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
			// Burada, ger�ek bir kimlik do�rulama i�lemi ger�ekle�tirmeniz gerekir.
			// Bu �rnekte basit bir �ifre kontrol� yap�l�yor.

			if (IsValidUser(username, password))
			{
				// Kullan�c� do�ruland�ysa, kullan�c� bilgilerini i�eren bir ClaimsIdentity olu�turun.
				var claims = new[] { new Claim(ClaimTypes.Name, username) };
				var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

				// Kullan�c�y� oturum a��k olarak i�aretle ve kimlik bilgilerini ta��yan bir �erez olu�turun.
				var principal = new ClaimsPrincipal(identity);
				await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

				// Giri� ba�ar�l�, ana sayfaya y�nlendirin veya ba�ka bir sayfaya y�nlendirin.
				return RedirectToAction("Index", "Home");
			}

			// Kullan�c� do�rulanamad�, hata mesaj�n� g�sterin veya ba�ka bir i�lem yap�n.
			ModelState.AddModelError(string.Empty, "Ge�ersiz kullan�c� ad� veya �ifre");
			return View("Index");
		}

		public async Task<IActionResult> Logout()
		{
			// Kullan�c�y� oturumdan ��kar�n.
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

			// ��k�� yapt�ktan sonra ana sayfaya y�nlendirin veya ba�ka bir sayfaya y�nlendirin.
			return RedirectToAction("Index", "Home");
		}

		private bool IsValidUser(string username, string password)
		{
			// Bu b�l�m� kendi kimlik do�rulama mant���n�za g�re doldurun.
			// �rne�in, veritaban�nda kullan�c� ad� ve �ifre kontrol� yapabilirsiniz.

			// Bu �rnekte sadece basit bir kontrol var.
			return username == "demo" && password == "demo";
		}
	}
}
