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
    public class OduncController : Controller
    {
        // GET: Odunc
        Dbo_KutuphaneEntities db = new Dbo_KutuphaneEntities();
        [Authorize(Roles ="A")]
        public ActionResult Index(string p, int sayfa = 1)
        {
            var odunckitap = db.Tbl_Hareket.Where(x => x.IslemDurum == false) ;
            if (!string.IsNullOrEmpty(p))
            {
                odunckitap = odunckitap.Where(m => m.Tbl_Kitaplar.Ad.ToLower().Contains(p.ToLower()));
            }
            var viewModelList = odunckitap.ToList().Select(k => new Tbl_Uyeler
            {
                Ad = k.Tbl_Uyeler.Ad,
                Soyad = k.Tbl_Uyeler.Soyad
            }).ToList();
            return View(odunckitap.ToList().ToPagedList(sayfa, 10));
        }
        [HttpGet]
        public ActionResult OduncVer()
        {
            List<SelectListItem> deger1 = (from i in db.Tbl_Uyeler.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.TamAd,
                                               Value = i.ID.ToString(),
                                           }).ToList();

            ViewBag.dgr1 = deger1;
            List<SelectListItem> deger2 = (from i in db.Tbl_Kitaplar.Where(x=>x.Durum == true).ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.Ad,
                                               Value = i.ID.ToString(),
                                           }).ToList();

            ViewBag.dgr2 = deger2;
            List<SelectListItem> deger3 = (from i in db.Tbl_Personeller.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.Personel,
                                               Value = i.ID.ToString(),
                                           }).ToList();

            ViewBag.dgr3 = deger3;
            return View();
        }
        [HttpPost]
        public ActionResult OduncVer(Tbl_Hareket h)
        {
            var uye = db.Tbl_Uyeler.Where(u => u.ID == h.Tbl_Uyeler.ID).FirstOrDefault();
            var ktp = db.Tbl_Kitaplar.Where(k => k.ID == h.Tbl_Kitaplar.ID).FirstOrDefault();
            var prsn = db.Tbl_Personeller.Where(p => p.ID == h.Tbl_Personeller.ID).FirstOrDefault();
            h.Tbl_Uyeler = uye;
            h.Tbl_Kitaplar = ktp;
            h.Tbl_Personeller = prsn;
            db.Tbl_Hareket.Add(h);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult OduncIade(Tbl_Hareket p)
        {
            var odnc = db.Tbl_Hareket.Find(p.ID);
            DateTime d1 = DateTime.Parse(odnc.IadeTarih.ToString());
            DateTime d2 = DateTime.Parse(DateTime.Now.ToShortDateString());
            TimeSpan d3 = d2 - d1;
            ViewBag.dgr = d3.TotalDays; 
            return View("OduncIade",odnc);
        }
        public ActionResult OduncGuncelle(Tbl_Hareket h)
        {
            var hareket = db.Tbl_Hareket.Find(h.ID);
            hareket.UyeninGetirTarih = h.UyeninGetirTarih;
            hareket.IslemDurum = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}