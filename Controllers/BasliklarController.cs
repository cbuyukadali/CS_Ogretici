using Microsoft.AspNetCore.Mvc;

using CS_Ogretici.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using System.Linq.Expressions;

using Microsoft.Extensions.FileProviders;

//Resim Listesini Görüntülemek için bak.
//https://codepedia.info/get-image-file-from-wwwroot-folder-aspnet-core


namespace CS_Ogretici.Controllers
{
    [Authorize]
    public class BasliklarController : Controller
    {
        Context c = new Context();

        [HttpGet]
        public IActionResult ResimYukle()
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
                ViewBag.Message = null;
                return View();
            }
            else
                return RedirectToAction("Hosgeldiniz","Basliklar");
        }

        [HttpPost]
        public IActionResult ResimYukle(IFormFile file)
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
                if (file == null)
                {
                    ViewBag.Message = "Resim dosyası yüklenemedi!!";
                    return View();
                }
                else {
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img");
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);


                    string fileNameWithPath = Path.Combine(path, file.FileName);

                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }                  

                    ViewBag.Message = "Resim dosyası başarıyla yüklendi!!";
                    return View();
                }
            }
            else { 
                return RedirectToAction("Hosgeldiniz", "Basliklar");
            }
        }

        //[HttpGet]
        //public IActionResult ResimListele()
        //{

        //https://codepedia.info/get-image-file-from-wwwroot-folder-aspnet-core

        //}

        public IActionResult Hakkinda()
        {

            var degerler = c.Basliklars.ToList();
            return View(degerler);

        }


        public IActionResult Hosgeldiniz()
        {
            if (HttpContext.Session.GetString("Tur") == null)
            {
                return RedirectToAction("Logout","Home"); //Programdan düzgün çıkış yapılmadan çıkıldığında, kayıtsız gezintili çerezleri logout a yönlendir
            }
            else
            {

                var dep = c.Basliklars.FirstOrDefault();
                if(dep.BaslikSayfa != null)
                {
                    ViewBag.Sayfa = dep.BaslikSayfa;
                }
                // View sadece bir degisken yollamaya izin verdigi icin model listesini view ile kullanilacak kismi ViewBag ile yolluyoruz
                //var dep = c.Basliklars.Find(3);//ilk sayfaya yonlendir
                //ViewBag.Sayfa = dep.BaslikSayfa;
                var degerler = c.Basliklars.ToList();
                return View(degerler);
            }
        }

        [HttpGet]
        public IActionResult SayfaGoster(int id)
        {
                // View sadece bir degisken yollamaya izin verdigi icin model listesini view ile kullanilacak kismi ViewBag ile yolluyoruz
            var dep = c.Basliklars.Find(id);
            if (dep == null)
            {
                return RedirectToAction("Hosgeldiniz", "Basliklar");
            }
            else
            {
                TempData["Sayfa"] = dep.BaslikSayfa;

                TempData["Parid"] = dep.BaslikParentId; //Hangi linke tiklandigina hatirlariz ve sayfa yuklendikten sonra acik tutariz

                //Main Sidebar kismindaki listeye model gondermek icin eklendi
                var degerler = c.Basliklars.ToList();
                //

                return View("SayfaGoster", degerler);
            }
        }

        public IActionResult AnaBaslikListele()
        {
            if (HttpContext.Session.GetString("Tur") == "SuperAdmin")
            {
                var degerler = c.Basliklars.ToList();
                return View(degerler);
            }
            else
                return RedirectToAction("Hosgeldiniz");
        }
        public IActionResult TumBaslikListele()
        {
            if (HttpContext.Session.GetString("Tur") == "SuperAdmin")
            {
                var degerler = c.Basliklars.ToList();
                return View(degerler);
            }
            else
                return RedirectToAction("Hosgeldiniz","Basliklar");
        }

        [HttpGet]
        public IActionResult YeniBaslik()
        {
            if (HttpContext.Session.GetString("Tur") == "SuperAdmin")
            {
                //Basliklar listesini ViewBag ile yollariz
                try
                {
                    List<Basliklar> BaslikListesi = c.Basliklars.ToList();
                    ViewBag.Basliklars = BaslikListesi;
                }
                catch(Exception ex)
                {
                    return RedirectToAction("Hosgeldiniz", "Basliklar");
                }
                //
                if (TempData["YeniBaslik"] != null)
                {
                    ViewBag.YeniBaslik = TempData["YeniBaslik"];
                    TempData["YeniBaslik"] = null;
                }

                return View();
            }
            else
                return RedirectToAction("Hosgeldiniz","Basliklar");
        }

        [HttpPost]
        public IActionResult YeniBaslik(Basliklar d)
        {
            if (HttpContext.Session.GetString("Tur") == "SuperAdmin")
            {
                if (d.BaslikSayfa == null || d.BaslikAd == null )
                {
                    TempData["YeniBaslik"] = "Başlık Adı veya Sayfası Boş Geçilemez!";
                }
                else
                {
                    TempData["YeniBaslik"] = "Sayfa Başarıyla Eklendi.";

                   c.Basliklars.Add(d);
                    c.SaveChanges();
                }
                //return RedirectToAction("Hosgeldiniz","Basliklar");
                return RedirectToAction("YeniBaslik","Basliklar");
            }
            else
                return RedirectToAction("Hosgeldiniz", "Basliklar");
        }


        public IActionResult BaslikSil(int id)
        {
            var dep = c.Basliklars.Find(id);
            if (dep != null)
            {
                if (HttpContext.Session.GetString("Tur") == "SuperAdmin")
                {
                    c.Basliklars.Remove(dep);
                    c.SaveChanges();
                }
            }
            return RedirectToAction("TumBaslikListele","Basliklar");
        }


        public IActionResult AltBaslikListele(int id)
        {
            if (HttpContext.Session.GetString("Tur") == "SuperAdmin")
            {
                var degerler = c.Basliklars.ToList();
                ViewBag.ParentId = id;
                return View(degerler);
            }
            else
                return RedirectToAction("Hosgeldiniz","Basliklar");
        }


        [HttpGet]
        public IActionResult BaslikGetir(int id)
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

                if (TempData["TBaslikGetir"] != null)
                {
                    ViewBag.VBaslikGetir = TempData["TBaslikGetir"];
                    TempData["YeniBaslik"] = null;
                }

                var dep = c.Basliklars.Find(id);
                return View("BaslikGetir", dep);
            }
            else
            {
                return RedirectToAction("Hosgeldiniz","Basliklar");
            }
        }
        [HttpPost]
        public IActionResult BaslikGetir(Basliklar d)
        {
            var dep = c.Basliklars.Find(d.BaslikId);
            if (dep != null)
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

                    if (d.BaslikSayfa == null || d.BaslikAd == null)
                    {
                        TempData["TBaslikGetir"] = "Başlık Adı veya Sayfası Boş Geçilemez!";
                    }
                    else
                    {
                        TempData["TBaslikGetir"] = "Sayfa Başarıyla Güncellendi.";

                        
                            dep.BaslikAd = d.BaslikAd;

                        
                        dep.BaslikSayfa = d.BaslikSayfa;

                        dep.BaslikParentId = d.BaslikParentId;
                        c.SaveChanges();
                    }
                    return RedirectToAction("BaslikGetir", "Basliklar");
                }
            }
            return RedirectToAction("Hosgeldiniz","Basliklar");
        }
    }
}
