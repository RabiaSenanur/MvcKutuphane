using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;
namespace MvcKutuphane.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        Dbo_KutuphaneEntities db = new Dbo_KutuphaneEntities();
        public ActionResult Index()
        {
            var degerler = db.Tbl_Kategoriler.Where(x => x.Durum == true).ToList();
            return View(degerler);
        }
        public ActionResult PasifKategori()
        {
            var degerler = db.Tbl_Kategoriler.Where(x => x.Durum == false).ToList();
            return View(degerler);
        }
        public ActionResult KategoriAktifEt(int id)
        {
            var kategori = db.Tbl_Kategoriler.Find(id);
            kategori.Durum = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult KategoriEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult KategoriEkle(Tbl_Kategoriler yeniKat)
        {
            db.Tbl_Kategoriler.Add(yeniKat);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KategoriSil(int id)
        {
            var kategori = db.Tbl_Kategoriler.Find(id);
            kategori.Durum = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KategoriGetir(int id)
        {
            var kategori = db.Tbl_Kategoriler.Find(id);
            return View("KategoriGetir",kategori);
        }
        public ActionResult KategoriGuncelle(Tbl_Kategoriler p)
        {
            var kategori = db.Tbl_Kategoriler.Find(p.KategoriID);
            kategori.KategoriAd = p.KategoriAd;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}