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
    public class IslemController : Controller
    {
        // GET: Islem
        Dbo_KutuphaneEntities db = new Dbo_KutuphaneEntities();
        public ActionResult Index(string p, int sayfa = 1)
        {
            var odunckitap = db.Tbl_Hareket.Where(x => x.IslemDurum == true);
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

    }
}