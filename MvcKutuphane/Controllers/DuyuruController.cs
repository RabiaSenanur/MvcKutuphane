using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;
namespace MvcKutuphane.Controllers
{
    public class DuyuruController : Controller
    {
        // GET: Duyuru
        Dbo_KutuphaneEntities db = new Dbo_KutuphaneEntities();
        public ActionResult Index()
        {
            var degerler = db.Tbl_Duyurular.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniDuyuru()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniDuyuru(Tbl_Duyurular d)
        {
            db.Tbl_Duyurular.Add(d);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DuyuruSil(int id)
        {
            var duyuru = db.Tbl_Duyurular.Find(id);
            db.Tbl_Duyurular.Remove(duyuru);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DuyuruDetay(Tbl_Duyurular d)
        {
            var dty = db.Tbl_Duyurular.Find(d.ID);
            return View("DuyuruDetay",dty);

        }
        public ActionResult DuyuruGuncelle(Tbl_Duyurular d)
        {
            var duyuru = db.Tbl_Duyurular.Find(d.ID);
            duyuru.Kategori = d.Kategori;
            duyuru.Icerik = d.Icerik;
            duyuru.Tarih = d.Tarih;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}