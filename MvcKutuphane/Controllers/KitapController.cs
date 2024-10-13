using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;
namespace MvcKutuphane.Controllers
{
    public class KitapController : Controller
    {
        // GET: Kitap
        Dbo_KutuphaneEntities db = new Dbo_KutuphaneEntities();
        public ActionResult Index(string p)
        {
            var kitaplar = from k in db.Tbl_Kitaplar select k;
            if (!string.IsNullOrEmpty(p))
            {
                kitaplar = kitaplar.Where(m => m.Ad.ToLower().Contains(p.ToLower()));
            }
            return View(kitaplar.ToList());
        }
        [HttpGet]// sayfa yüklendiğinde değerleri döndürsün
        public ActionResult KitapEkle()
        {
            List<SelectListItem> deger1 = (from i in db.Tbl_Kategoriler.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.KategoriAd,
                                               Value = i.KategoriID.ToString(),
                                           }).ToList();
            ViewBag.dgr1 = deger1;// Kategorileri getiriyor bu sistem

            List<SelectListItem> deger2 = (from i in db.Tbl_Yazarlar.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.Ad +' '+ i.Soyad,
                                               Value = i.ID.ToString(),
                                           }).ToList();
           
            ViewBag.dgr2 = deger2;//Yazarları getiriyor
            return View();
        }
        [HttpPost]
        public ActionResult KitapEkle(Tbl_Kitaplar p)
        {
            var ktg = db.Tbl_Kategoriler.Where(k => k.KategoriID == p.Tbl_Kategoriler.KategoriID).FirstOrDefault();
            var yzr = db.Tbl_Yazarlar.Where(y => y.ID == p.Tbl_Yazarlar.ID).FirstOrDefault();
            p.Tbl_Kategoriler = ktg;
            p.Tbl_Yazarlar = yzr;
            db.Tbl_Kitaplar.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KitapSil(int id)
        {
            var kitap = db.Tbl_Kitaplar.Find(id);
            db.Tbl_Kitaplar.Remove(kitap);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KitapGetir(int id)
        {
            var kitap = db.Tbl_Kitaplar.Find(id);
            List<SelectListItem> deger1 = (from i in db.Tbl_Kategoriler.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.KategoriAd,
                                               Value = i.KategoriID.ToString(),
                                           }).ToList();
            ViewBag.dgr1 = deger1;

            List<SelectListItem> deger2 = (from i in db.Tbl_Yazarlar.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.Ad + ' ' + i.Soyad,
                                               Value = i.ID.ToString(),
                                           }).ToList();

            ViewBag.dgr2 = deger2;
            return View("KitapGetir", kitap);
        }
        public ActionResult KitapGuncelle(Tbl_Kitaplar p)
        {
            var kitap = db.Tbl_Kitaplar.Find(p.ID);
            kitap.Ad = p.Ad;
            kitap.BasımYıl = p.BasımYıl;
            kitap.Sayfa = p.Sayfa;
            kitap.Yayınevi = p.Yayınevi;
            kitap.Durum = true;
            var ktg = db.Tbl_Kategoriler.Where(k => k.KategoriID == p.Tbl_Kategoriler.KategoriID).FirstOrDefault();
            var yzr = db.Tbl_Yazarlar.Where(y => y.ID == p.Tbl_Yazarlar.ID).FirstOrDefault();
            kitap.Yazar = yzr.ID;
            kitap.Kategori = ktg.KategoriID;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
    }
}