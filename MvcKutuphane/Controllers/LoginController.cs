using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;
using System.Web.Security;
using System.Net.Mail;
using System.Net;
namespace MvcKutuphane.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        // GET: Login
        Dbo_KutuphaneEntities db = new Dbo_KutuphaneEntities();
        [HttpGet]
        public ActionResult GirisYap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GirisYap(Tbl_Uyeler p)
        {
            var bilgiler = db.Tbl_Uyeler.FirstOrDefault(x => x.KullanıcıAdı == p.KullanıcıAdı && x.Sifre == p.Sifre);
            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.KullanıcıAdı, false);
                Session["KullanıcıAdı"] = bilgiler.KullanıcıAdı.ToString();
                return RedirectToAction("Index","Panelim");
            }else
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult ResetPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ResetPassword(Tbl_Uyeler t)
        {
            if (!string.IsNullOrEmpty(t.Mail))
            {
                try
                {
                    var fromAddress = new MailAddress("admin1@gmail.com", "Admin");
                    var toAddress = new MailAddress(t.Mail);
                    const string subject = "Şifre Sıfırlama";
                    const string body = "Lütfen aşağıdaki linki kullanarak şifrenizi sıfırlayın.";

                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(fromAddress.Address, "456")
                    };

                    using (var message = new MailMessage(fromAddress, toAddress)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        smtp.Send(message);
                    }

                    ViewBag.Message = "Mail gönderildi.";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Mail gönderilirken bir hata oluştu: " + ex.Message;
                }
            }

            return View();

        }
    }
}