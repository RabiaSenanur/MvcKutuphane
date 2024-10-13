using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models;
using MvcKutuphane.Models.Entity;
namespace MvcKutuphane.Controllers
{
    public class GrafikController : Controller
    {
        // GET: Grafik
        Dbo_KutuphaneEntities db = new Dbo_KutuphaneEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult VisualizeKitapResult()
        {
            return Json(liste());
        }
        public List<Class1> liste()
        {
            List<Class1> cs = new List<Class1>();

            // Her yayınevi için kitap sayısını çeker ve listeye ekler
            var yayinevleri = db.Tbl_Kitaplar
                                .GroupBy(k => k.Yayınevi)
                                .Select(g => new { Yayinevi = g.Key, Sayi = g.Count() })
                                .ToList();

            foreach (var item in yayinevleri)
            {
                cs.Add(new Class1()
                {
                    yayinevi = item.Yayinevi,
                    sayi = item.Sayi
                });
            }

            return cs;
        }
    }
}