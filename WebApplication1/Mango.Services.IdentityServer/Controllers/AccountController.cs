using Mango.Services.IdentityServer.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

public class AccountController : Controller
{
    public IActionResult Login()
    {
        return Challenge(new AuthenticationProperties { RedirectUri = "/" });
    }


    public IActionResult Register()
    {
        // Kayıt olma sayfası
        return View();
    }

	[HttpPost]
	public IActionResult Register(RegisterViewModel model)
	{
		if (ModelState.IsValid)
		{
			// Kullanıcı kayıt işlemleri
		}
		else
		{
			// ModelState içinde hata varsa burada işlem yapabilirsiniz
			var errors = ModelState.Values.SelectMany(v => v.Errors);
			// Hataları inceleyebilir veya loglayabilirsiniz
		}

		// Hata durumunda geri dön
		return View(model);
	}
	[HttpPost]
	public async Task<IActionResult> Login(string username, string password)
	{
		// Burada, gerçek bir kimlik doğrulama işlemi gerçekleştirmeniz gerekir.
		// Bu örnekte basit bir şifre kontrolü yapılıyor.

		if (IsValidUser(username, password))
		{
			// Kullanıcı doğrulandıysa, kullanıcı bilgilerini içeren bir ClaimsIdentity oluşturun.
			var claims = new[] { new Claim(ClaimTypes.Name, username) };
			var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

			// Kullanıcıyı oturum açık olarak işaretle ve kimlik bilgilerini taşıyan bir çerez oluşturun.
			var principal = new ClaimsPrincipal(identity);
			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

			// Giriş başarılı, ana sayfaya yönlendirin veya başka bir sayfaya yönlendirin.
			return RedirectToAction("Index", "Home");
		}

		// Kullanıcı doğrulanamadı, hata mesajını gösterin veya başka bir işlem yapın.
		ModelState.AddModelError(string.Empty, "Geçersiz kullanıcı adı veya şifre");
		return View("Index");
	}

	public async Task<IActionResult> Logout()
	{
		// Kullanıcıyı oturumdan çıkarın.
		await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

		// Çıkış yaptıktan sonra ana sayfaya yönlendirin veya başka bir sayfaya yönlendirin.
		return RedirectToAction("Index", "Home");
	}

	private bool IsValidUser(string username, string password)
	{
		// Bu bölümü kendi kimlik doğrulama mantığınıza göre doldurun.
		// Örneğin, veritabanında kullanıcı adı ve şifre kontrolü yapabilirsiniz.

		// Bu örnekte sadece basit bir kontrol var.
		return username == "demo" && password == "demo";
	}
}
