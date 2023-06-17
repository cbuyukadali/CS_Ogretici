using Microsoft.AspNetCore.Mvc;

using CS_Ogretici.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using System;

namespace CS_Ogretici.Controllers
{
    [Authorize]

    //Context.Session.GetString("Tur")

    public class KullanicilarController : Controller
    {
        Context c = new Context();

        [HttpGet]
        public IActionResult YeniKullanici()
        {
            //Basliklar listesini ViewBag ile yollariz
            try
            {
                List<Basliklar> BaslikListesi = c.Basliklars.ToList();
                ViewBag.Basliklars = BaslikListesi;
            }
            catch (Exception ex)
            {
                return RedirectToAction("Hosgeldiniz", "Basliklar");
            }
            //
            return View();
        }

		[HttpPost]
		public IActionResult YeniKullanici(Kullanicilar d)
		{
			var isEmailRegistered = c.Kullanicilars.Any(x => x.KullaniciEposta == d.KullaniciEposta);
			if (!isEmailRegistered)
			{
				c.Kullanicilars.Add(d);
				c.SaveChanges();
			}
			else
			{
				TempData["shortMessage"] = "Kullanıcı epostası zaten kayıtlı.";
			}

			return RedirectToAction("YeniKullanici", "Kullanicilar");

		}
		public IActionResult KullanicilarListele()
        {
            if (HttpContext.Session.GetString("Tur") == "SuperAdmin")
            {
                //Basliklar listesini ViewBag ile yollariz
                try
                {
                    List<Basliklar> BaslikListesi = c.Basliklars.ToList();
                    ViewBag.Basliklars = BaslikListesi;
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Hosgeldiniz", "Basliklar");
                }
                //

                var degerler = c.Kullanicilars.ToList();
                return View(degerler);
            }
            else
            {
                return RedirectToAction("Hosgeldiniz", "Basliklar");
            }
        }

        
        public IActionResult KullaniciSil(int id)
        {
            if (HttpContext.Session.GetString("Tur") == "SuperAdmin")
            {
                var dep = c.Kullanicilars.Find(id);
                if (dep != null)
                {
                    c.Kullanicilars.Remove(dep);
                    c.SaveChanges();
                }
                return RedirectToAction("KullanicilarListele", "Kullanicilar");
            }
            else
            {
                return RedirectToAction("Hosgeldiniz", "Basliklar");
            }
        }
             

        [HttpGet]
        public IActionResult KullaniciGetir(int id)
        {
            if (HttpContext.Session.GetString("Tur") == "SuperAdmin")
            {
                //Basliklar listesini ViewBag ile yollariz
                try
                {
                    List<Basliklar> BaslikListesi = c.Basliklars.ToList();
                    ViewBag.Basliklars = BaslikListesi;
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Hosgeldiniz", "Basliklar");
                }
                //

                var dep = c.Kullanicilars.Find(id);
                return View("KullaniciGetir", dep);
            }
            else
            {
                return RedirectToAction("Hosgeldiniz", "Basliklar");
            }
        }
        [HttpPost]
        public IActionResult KullaniciGetir(Kullanicilar b)
        {
            if (HttpContext.Session.GetString("Tur") == "SuperAdmin")
            {
                //Basliklar listesini ViewBag ile yollariz
                try
                {
                    List<Basliklar> BaslikListesi = c.Basliklars.ToList();
                    ViewBag.Basliklars = BaslikListesi;
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Hosgeldiniz", "Basliklar");
                }
                //

                var dep = c.Kullanicilars.Find(b.KullaniciId);
            if (dep != null)
            {
                try {
                    dep.KullaniciAd = b.KullaniciAd;
                    dep.KullaniciSoyad = b.KullaniciSoyad;
                    dep.KullaniciEposta = b.KullaniciEposta;
                    dep.KullaniciSifre = b.KullaniciSifre;
                    dep.KullaniciTur = b.KullaniciTur;
                    c.SaveChanges();
                }
                catch { }
                
            }
            return RedirectToAction("KullanicilarListele", "Kullanicilar");
            }
            else
            {
                return RedirectToAction("Hosgeldiniz", "Basliklar");
            }
        }
    }
}
