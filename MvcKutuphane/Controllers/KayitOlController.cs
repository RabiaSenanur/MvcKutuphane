using MvcKutuphane.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcKutuphane.Controllers
{
    [AllowAnonymous]
    public class KayitOlController : Controller
    {
        // GET: KayitOl
        Dbo_KutuphaneEntities db = new Dbo_KutuphaneEntities();
        [HttpGet]
        public ActionResult Kayit()
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
        public ActionResult Kayit(Tbl_Uyeler p)
        {
            List<SelectListItem> egitim = (from i in db.Tbl_Egitimler.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.Egitim,
                                               Value = i.ID.ToString(),
                                           }).ToList();
            ViewBag.egitim1 = egitim;
            var egt = db.Tbl_Egitimler.Where(e => e.ID == p.Tbl_Egitimler.ID).FirstOrDefault();
            p.Tbl_Egitimler = egt;
            db.Tbl_Uyeler.Add(p);
            db.SaveChanges();            
            return View();
        }
    }
}