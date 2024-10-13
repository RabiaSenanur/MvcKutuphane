using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcKutuphane.Models.Entity;

namespace MvcKutuphane.Controllers
{
    [Authorize]
    public class PanelimController : Controller
    {
        // GET: Panelim
        Dbo_KutuphaneEntities db = new Dbo_KutuphaneEntities();

        [HttpGet]
        public ActionResult Index()
        {
            var uyeKullaniciAdi = (string)Session["KullanıcıAdı"];
            var tamAd = db.Tbl_Uyeler
                .Where(x => x.KullanıcıAdı == uyeKullaniciAdi)
                .Select(y => y.TamAd)
                .FirstOrDefault();

            Session["AdSoyad"] = tamAd;
            //var degerler = db.Tbl_Uyeler.FirstOrDefault(x => x.KullanıcıAdı == uyeKullaniciAdi);
            var degerler = db.Tbl_Duyurular.ToList();
            var d1 = db.Tbl_Uyeler.Where(x => x.KullanıcıAdı == uyeKullaniciAdi).Select(y => y.TamAd).FirstOrDefault();
            ViewBag.d1 = d1;
            var d2 = db.Tbl_Uyeler.Where(x => x.KullanıcıAdı == uyeKullaniciAdi).Select(y => y.Fotoğraf).FirstOrDefault();
            ViewBag.d2 = d2;
            ViewBag.d3 = uyeKullaniciAdi;
            var d4 = db.Tbl_Uyeler.Where(x => x.KullanıcıAdı == uyeKullaniciAdi).Select(x => x.Tbl_Egitimler.Egitim).FirstOrDefault(); ;
            ViewBag.d4 = d4;
            var d5 = db.Tbl_Uyeler.Where(x => x.KullanıcıAdı == uyeKullaniciAdi).Select(x => x.Telefon).FirstOrDefault();
            ViewBag.d5 = d5;
            var d6 = db.Tbl_Uyeler.Where(x => x.KullanıcıAdı == uyeKullaniciAdi).Select(x => x.Mail).FirstOrDefault();
            ViewBag.d6 = d6;
            var uyeid = db.Tbl_Uyeler.Where(x => x.KullanıcıAdı == uyeKullaniciAdi).Select(y => y.ID).FirstOrDefault();
            var d7 = db.Tbl_Hareket.Where(x => x.Uye == uyeid).Count();
            ViewBag.d7 = d7;
            var d8 = db.Tbl_Mesajlar.Where(x => x.Alıcı == uyeKullaniciAdi).Count();
            ViewBag.d8 = d8;
            var d9 = db.Tbl_Duyurular.Count();
            ViewBag.d9 = d9;
            List<SelectListItem> egitim = (from i in db.Tbl_Egitimler.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.Egitim,
                                               Value = i.ID.ToString(),
                                           }).ToList();
            ViewBag.egitim1 = egitim;

            return View(degerler);
        }
        [HttpPost]
        public ActionResult Index(Tbl_Uyeler p)
        {
            List<SelectListItem> egitim = (from i in db.Tbl_Egitimler.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.Egitim,
                                               Value = i.ID.ToString(),
                                           }).ToList();
            ViewBag.egitim1 = egitim;
            var kullanici = (string)Session["KullanıcıAdı"];
            var uye = db.Tbl_Uyeler.FirstOrDefault(x => x.KullanıcıAdı == kullanici);
            uye.Sifre = p.Sifre;
            uye.Ad = p.Ad;
            uye.Soyad = p.Soyad;
            uye.Mail = p.Mail;
            uye.KullanıcıAdı = p.KullanıcıAdı;
            uye.Telefon = p.Telefon;
            uye.Fotoğraf = p.Fotoğraf;
            var egt = db.Tbl_Egitimler.Where(e => e.ID == p.Tbl_Egitimler.ID).FirstOrDefault();
            uye.EgitimDurumu = egt.ID;
            db.SaveChanges();
            return View();
        }
        public ActionResult Kitaplarim()
        {
            var kullanici = Session["KullanıcıAdı"].ToString();
            var id = db.Tbl_Uyeler.Where(x => x.KullanıcıAdı == kullanici.ToString()).Select(z => z.ID).FirstOrDefault();
            var degerler = db.Tbl_Hareket.Where(x => x.Uye == id);
            return View(degerler.ToList());
        }

        public ActionResult Duyurular()
        {
            var duyuruListesi = db.Tbl_Duyurular.ToList();
            return View(duyuruListesi);
        }
        public ActionResult CikisYap()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("GirisYap", "Login");
        }
        public PartialViewResult Partial1()
        {
            return PartialView();
        }

        public PartialViewResult Partial2()
        {
            var kullaniciAdi = (string)Session["KullanıcıAdı"];
            var id = db.Tbl_Uyeler.Where(x => x.KullanıcıAdı == kullaniciAdi).Select(y => y.ID).FirstOrDefault();
            var uyebul = db.Tbl_Uyeler.Find(id);
            List<SelectListItem> egitim = (from i in db.Tbl_Egitimler.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.Egitim,
                                               Value = i.ID.ToString(),
                                           }).ToList();
            ViewBag.egitim1 = egitim;
            return PartialView("Partial2", uyebul);
        }
    }
}