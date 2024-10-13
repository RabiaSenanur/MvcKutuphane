using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;
namespace MvcKutuphane.Controllers
{
    public class AyarlarController : Controller
    {
        // GET: Ayarlar
        Dbo_KutuphaneEntities db = new Dbo_KutuphaneEntities();
        public ActionResult Index()
        {
            var kullanicilar = db.Tbl_Admin.ToList();
            return View(kullanicilar);
        }
        public ActionResult AdminEkle()
        {

            return View();
        }
        [HttpPost]
        public ActionResult AdminEkle(Tbl_Admin a)
        {
            db.Tbl_Admin.Add(a);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult AdminSil(int id)
        {
            var admin = db.Tbl_Admin.Find(id);
            db.Tbl_Admin.Remove(admin);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult AdminGuncelle(int id)
        {
            var admin = db.Tbl_Admin.Find(id);
            return View("AdminGuncelle", admin);
        }
        [HttpPost]
        public ActionResult AdminGuncelle(Tbl_Admin a)
        {
            var admin = db.Tbl_Admin.Find(a.ID);
            admin.Kullanıcı = a.Kullanıcı;
            admin.Sifre = a.Sifre;
            admin.Yetki = a.Yetki;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}