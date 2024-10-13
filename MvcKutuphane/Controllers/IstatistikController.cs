using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;
namespace MvcKutuphane.Controllers
{
    public class IstatistikController : Controller
    {
        // GET: Istatistik
        Dbo_KutuphaneEntities db = new Dbo_KutuphaneEntities();
        public ActionResult Index()
        {
            var deger1 = db.Tbl_Uyeler.Count();
            var deger2 = db.Tbl_Kitaplar.Count();
            var deger3 = db.Tbl_Kitaplar.Where(x => x.Durum == false).Count();
            var deger4 = db.Tbl_Cezalar.Where(x => x.Para > 0).Sum(x => x.Para);
            ViewBag.dgr1 = deger1;
            ViewBag.dgr2 = deger2;
            ViewBag.dgr3 = deger3;
            ViewBag.dgr4 = deger4;
            return View();
        }
        public ActionResult Galeri()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ResimYukle(HttpPostedFileBase dosya)
        {
            if (dosya == null || dosya.ContentLength == 0)
            {
                TempData["Message"] = "Lutfen bir resim secin.";
                return RedirectToAction("Galeri");
            }

            string dosyayolu = Path.Combine(Server.MapPath("~/web2/resimler/"), Path.GetFileName(dosya.FileName));
            dosya.SaveAs(dosyayolu);

            TempData["Message"] = "Resim basariyla yuklendi.";
            return RedirectToAction("Galeri");
        }
        public ActionResult LinqKart()
        {
            var deger1 = db.Tbl_Kitaplar.Count();
            var deger2 = db.Tbl_Uyeler.Count();
            var deger3 = db.Tbl_Cezalar.Where(x => x.Para > 0).Sum(x => x.Para);
            var deger4 = db.Tbl_Kitaplar.Where(x => x.Durum == false).Count();
            var deger5 = db.Tbl_Kategoriler.Count();
            var deger8 = db.EnFazlaKitapYazar().FirstOrDefault();
            var deger9 = db.Tbl_Kitaplar.GroupBy(book => book.Yayınevi).OrderByDescending(group => group.Count()).Select(group => group.Key).FirstOrDefault();
            var deger10 = db.Tbl_Hareket.GroupBy(x => x.Tbl_Uyeler.TamAd).OrderByDescending(z => z.Count()).Select(y =>  y.Key ).FirstOrDefault();
            var deger11 = db.Tbl_Iletisim.Count();
            var deger12 = db.Tbl_Hareket.GroupBy(x => x.Tbl_Personeller.Personel).OrderByDescending(z => z.Count()).Select(y => y.Key).FirstOrDefault();

            ViewBag.dgr1 = deger1;
            ViewBag.dgr2 = deger2;
            ViewBag.dgr3 = deger3;
            ViewBag.dgr4 = deger4;
            ViewBag.dgr5 = deger5;
            ViewBag.dgr8 = deger8;
            ViewBag.dgr9 = deger9;
            ViewBag.dgr10 = deger10;
            ViewBag.dgr11 = deger11;
            ViewBag.dgr12 = deger12;
            return View();
        }
    }
}