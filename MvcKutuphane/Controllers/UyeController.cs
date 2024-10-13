using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;
using PagedList;
using PagedList.Mvc;
namespace MvcKutuphane.Controllers
{
    public class UyeController : Controller
    {
        // GET: Uye
        Dbo_KutuphaneEntities db = new Dbo_KutuphaneEntities();
        public ActionResult Index(int sayfa = 1)
        {
            var degerler = db.Tbl_Uyeler.ToList().ToPagedList(sayfa,10);
            return View(degerler);
        }
        [HttpGet]
        public ActionResult UyeEkle()
        {
            List<SelectListItem> egitim = (from i in db.Tbl_Egitimler.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.Egitim,
                                               Value = i.ID.ToString(),
                                           }).ToList();
            ViewBag.egitim1 = egitim;
            return View();
        }
        [HttpPost]
        public ActionResult UyeEkle(Tbl_Uyeler p)
        {
            if (!ModelState.IsValid)
            {
                var egt = db.Tbl_Egitimler.Where(k => k.ID == p.Tbl_Egitimler.ID).FirstOrDefault();
                p.EgitimDurumu = egt.ID;
                return View(p);
            }
            db.Tbl_Uyeler.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UyeSil(int id)
        {
            var uye = db.Tbl_Uyeler.Find(id);
            db.Tbl_Uyeler.Remove(uye);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UyeGetir(int id)
        {
            var uye = db.Tbl_Uyeler.Find(id);
            List<SelectListItem> egitim = (from i in db.Tbl_Egitimler.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.Egitim,
                                               Value = i.ID.ToString(),
                                           }).ToList();
            ViewBag.egitim1 = egitim;
            return View("UyeGetir", uye);
        }
        [HttpPost]
        public ActionResult UyeGuncelle(Tbl_Uyeler u)
        {
            var uye = db.Tbl_Uyeler.Find(u.ID);
            uye.Ad = u.Ad;
            uye.Soyad = u.Soyad;
            uye.Mail = u.Mail;
            uye.KullanıcıAdı = u.KullanıcıAdı;
            uye.Sifre = u.Sifre;
            uye.Telefon = u.Telefon;
            uye.EgitimDurumu = u.EgitimDurumu;
            uye.Fotoğraf = u.Fotoğraf;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UyeKitapGecmis(int id)
        {
            var ktpgcms = db.Tbl_Hareket.Where(x => x.Uye == id).ToList();
            var uyekit = db.Tbl_Uyeler.Where(y => y.ID == id).Select(z => z.Ad + " " + z.Soyad).FirstOrDefault();
            ViewBag.uye = uyekit;
            return View(ktpgcms);
        }
    }
}