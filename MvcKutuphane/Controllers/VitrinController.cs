using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;
using MvcKutuphane.Models.Siniflarim;
namespace MvcKutuphane.Controllers
{
    [AllowAnonymous]
    public class VitrinController : Controller
    {
        // GET: Vitrin
        Dbo_KutuphaneEntities db = new Dbo_KutuphaneEntities();
        [HttpGet]
        public ActionResult Index()
        {
            Class1 cs = new Class1();
            cs.Deger1 = db.Tbl_Kitaplar.ToList();
            cs.Deger2 = db.Tbl_Hakkımızda.ToList();
            //var degerler = db.Tbl_Kitaplar.ToList();
            return View(cs);
        }
        [HttpPost]
        public ActionResult Index(Tbl_Iletisim t)
        {
            db.Tbl_Iletisim.Add(t);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
    }
}