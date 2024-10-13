using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;
namespace MvcKutuphane.Controllers
{
    public class PersonelController : Controller
    {
        Dbo_KutuphaneEntities db = new Dbo_KutuphaneEntities();
        // GET: Personel
        public ActionResult Index()
        {
            var degerler = db.Tbl_Personeller.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult PersonelEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult PersonelEkle(Tbl_Personeller p)
        {
            if (!ModelState.IsValid)
            {
                return View(p);
            }
            db.Tbl_Personeller.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult PersonelSil(int id)
        {
            var prs = db.Tbl_Personeller.Find(id);
            db.Tbl_Personeller.Remove(prs);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult PersonelGetir(int id)
        {
            var prs = db.Tbl_Personeller.Find(id);
            return View("PersonelGetir", prs);
        }
        public ActionResult PersonelGuncelle(Tbl_Personeller p)
        {
            var prs = db.Tbl_Personeller.Find(p.ID);
            prs.Personel = p.Personel;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}