using CS_Ogretici.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging.Signing;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Security.Claims;

namespace CS_Ogretici.Controllers
{

    [Authorize]
    public class HomeController : Controller
    {
        Context c = new Context();


        public const string SessionKeyName = "_Name";
        public const string SessionKeyType = "_Type";


        // Oturum Bilgileri icin eklendi. ***

        // https://learn.microsoft.com/en-us/aspnet/core/fundamentals/app-state?view=aspnetcore-7.0

        //***

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        //public IActionResult Index()
        // {
        //    return View();
        //}
        [AllowAnonymous]
        public IActionResult Login()
        {
            if(TempData["shortMessage"]!=null)
            {
                ViewBag.msg = TempData["shortMessage"].ToString();
                TempData["shortMessage"] = null;

            }

            return View();
        }


        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(string userEmail, string password)
        {
            var bilgiler = c.Kullanicilars.FirstOrDefault(x => x.KullaniciEposta == userEmail
            && x.KullaniciSifre == password);

            if (bilgiler != null)
            {
                ViewBag.msg = null;
                    
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, userEmail)
                };
                var claimsIdentity = new ClaimsIdentity(claims, "Login");

               

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));




                // Kullanici ad ve turunu tespit ederek kisitlamalar getirmek icin eklendi
                if (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionKeyName)))
                {
                    HttpContext.Session.SetString("Ad", bilgiler.KullaniciAd);
                    HttpContext.Session.SetString("Tur", bilgiler.KullaniciTur);
                   // HttpContext.Session.SetString("Time",bilgiler.)
               }
                //var name1 = HttpContext.Session.GetString("Ad");
                //var tur1 = HttpContext.Session.GetString("Tur");

            //    _logger.LogInformation("Session Name: {Name}", name1);
            //    _logger.LogInformation("Session Type: {Type}", tur1);
                //


                return RedirectToAction("Hosgeldiniz","Basliklar");
            }
            else
            {
                ViewBag.msg = "E-Posta veya Şifre Hatalı !";
                return View();
            }
        }
        /*
        //Session bağlantısı olmadan sınırsız süre için Autorize Cookies i oluşurur 
        {
            var bilgiler = c.Kullanicilars.FirstOrDefault(x => x.KullaniciEposta == userEmail
            && x.KullaniciSifre == password);   

            if (bilgiler!=null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, userEmail)
                };
                var claimsIdentity = new ClaimsIdentity(claims, "Login");

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, 
                    new ClaimsPrincipal(claimsIdentity));
                return Redirect(ReturnUrl == null ? "/Basliklar" : ReturnUrl);
            }
            else
                return View();
        }
      */

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Home");
        }


        [AllowAnonymous]
        [HttpGet]
        public IActionResult YeniKullanici()
        {

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult YeniKullanici(Kullanicilar d)
        {
            var isEmailRegistered = c.Kullanicilars.Any(x => x.KullaniciEposta == d.KullaniciEposta);
            if (!isEmailRegistered )
            {
                    c.Kullanicilars.Add(d);
                    c.SaveChanges();
            }
            else
            {
                TempData["shortMessage"] = "Kullanıcı epostası zaten kayıtlı.";                
            }

            return RedirectToAction("Login", "Home");

        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}