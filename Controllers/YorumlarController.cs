using CS_Ogretici.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using System.Diagnostics.Eventing.Reader;

namespace CS_Ogretici.Controllers
{
    [Authorize]
    public class YorumlarController : Controller
    {

        Context c = new Context();
        public IActionResult YorumListele()
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

            var degerler = c.Yorumlars.ToList();
            return View(degerler);
        }

        [HttpGet]
        public IActionResult YorumEkle()
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
            //}
        }

        [HttpPost]
        public IActionResult YorumEkle(Yorumlar d)
        {
            d.YorumYazar = HttpContext.Session.GetString("Ad");
            d.YorumParentId = 11;
            if (d.YorumYazar!=null || d.YorumSayfa !=null )
            {

                c.Yorumlars.Add(d);
                c.SaveChanges();
            }
            //
            //https://stackoverflow.com/questions/10013313/why-is-sql-server-throwing-this-error-cannot-insert-the-value-null-into-column
            //

            return RedirectToAction("YorumEkle", "Yorumlar");

        }

        public IActionResult YorumSil(int id)
        {
            var dep = c.Yorumlars.Find(id);
            if (dep != null)
            {
                if (HttpContext.Session.GetString("Tur") == "SuperAdmin")
                {
                    c.Yorumlars.Remove(dep);
                    c.SaveChanges();
                }
            }
            return RedirectToAction("YorumListele", "Yorumlar");
        }

        [HttpGet]
        public IActionResult YorumOnayla(int id)
        {
                var dep = c.Yorumlars.Find(id);

                if (dep == null)
                {
                    return RedirectToAction("Hosgeldiniz", "Basliklar");
                }
                else
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

                        return View("YorumOnayla", dep);
                    }
                    else
                        return RedirectToAction("Hosgeldiniz", "Basliklar");
                }
        }

        [HttpPost]
        public IActionResult YorumOnayla(Yorumlar b)
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

                var dep = c.Yorumlars.Find(b.YorumId);
                if (dep != null)
                {
                    try
                    {
                        dep.YorumYazar = b.YorumYazar;
                        dep.YorumParentId = b.YorumParentId;
                        dep.YorumSayfa = b.YorumSayfa;
                        dep.YorumTime = b.YorumTime;
                        dep.YorumAktif = b.YorumAktif;
                        c.SaveChanges();
                    }
                    catch { }

                }
                return RedirectToAction("YorumListele", "Yorumlar");
            }
            else
            {
                return RedirectToAction("Hosgeldiniz","Basliklar");
            }
        }
    }
}

