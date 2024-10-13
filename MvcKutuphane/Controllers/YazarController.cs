using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcKutuphane.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace MvcKutuphane.Controllers
{
    public class YazarController : Controller
    {
        // GET: Yazar
        Dbo_KutuphaneEntities db = new Dbo_KutuphaneEntities();
        public ActionResult Index(int sayfa = 1)
        {
            var degerler = db.Tbl_Yazarlar.ToList().ToPagedList(sayfa, 10);
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YazarEkle()
        {
            return View();
        }
        public ActionResult YazarEkle(Tbl_Yazarlar p)
        {
            if (!ModelState.IsValid)
            {
                return View("YazarEkle");
            }
            db.Tbl_Yazarlar.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult YazarSil(int id)
        {
            var yazar = db.Tbl_Yazarlar.Find(id);
            db.Tbl_Yazarlar.Remove(yazar);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult YazarGetir(int id)
        {
            var yzr = db.Tbl_Yazarlar.Find(id);
            return View("YazarGetir",yzr);
        }
        public ActionResult YazarGuncelle(Tbl_Yazarlar p)
        {
            var yzr = db.Tbl_Yazarlar.Find(p.ID);
            yzr.Ad = p.Ad;
            yzr.Soyad = p.Soyad;
            yzr.Detay = p.Detay;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult YazarKitaplar(int id)
        {
            var yzr = db.Tbl_Kitaplar.Where(x => x.Yazar == id).ToList();
            var yzrAd = db.Tbl_Yazarlar.Where(y =>y.ID == id).Select(z => z.Ad + " " +z.Soyad).FirstOrDefault();
            ViewBag.Yzr = yzrAd;
            return View(yzr);
        }
    }
}