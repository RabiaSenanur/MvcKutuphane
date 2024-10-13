using MvcKutuphane.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Siniflarim;

namespace MvcKutuphane.Controllers
{
    public class EgitimController : Controller
    {
        // GET: Egitim
        Dbo_KutuphaneEntities db = new Dbo_KutuphaneEntities();
        public ActionResult Index()
        {
            List<SelectListItem> egitim = (from i in db.Tbl_Egitimler.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.Egitim,
                                               Value = i.ID.ToString(),
                                           }).ToList();
            ViewBag.egitimler = egitim;
            return View();
        }
    }
}