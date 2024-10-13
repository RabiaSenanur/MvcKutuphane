using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;
namespace MvcKutuphane.Controllers
{
    public class MesajController : Controller
    {
        // GET: Mesaj
        Dbo_KutuphaneEntities db = new Dbo_KutuphaneEntities();
        
        public ActionResult Index()
        {
            var uyeKullaniciAdi = Session["KullanıcıAdı"].ToString();
            var mesajlar = db.Tbl_Mesajlar.Where(x => x.Alıcı == uyeKullaniciAdi.ToString()).ToList();
            return View(mesajlar);
        }
        public ActionResult Giden()
        {
            var uyeKullaniciAdi = Session["KullanıcıAdı"].ToString();
            var mesajlar = db.Tbl_Mesajlar.Where(x => x.Gönderen == uyeKullaniciAdi.ToString()).ToList();
            return View(mesajlar);
        }
        [HttpGet]
        public ActionResult YeniMesaj()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniMesaj(Tbl_Mesajlar t)
        {
            var uyeKullaniciAdi = Session["KullanıcıAdı"].ToString();
            t.Gönderen = uyeKullaniciAdi.ToString();
            t.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            db.Tbl_Mesajlar.Add(t);
            db.SaveChanges();
            return RedirectToAction("Giden","Mesaj");
        }
        public PartialViewResult Partial1()
        {
            var uyeKullaniciAdi = Session["KullanıcıAdı"].ToString();
            var gelenSayisi = db.Tbl_Mesajlar.Where(x =>x.Alıcı == uyeKullaniciAdi).Count();
            ViewBag.d1 = gelenSayisi;
            var gidenSayisi = db.Tbl_Mesajlar.Where(x => x.Gönderen == uyeKullaniciAdi).Count();
            ViewBag.d2 = gidenSayisi;
            return PartialView();
        }
    }
}