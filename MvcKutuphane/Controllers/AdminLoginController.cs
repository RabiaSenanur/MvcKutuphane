using MvcKutuphane.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcKutuphane.Controllers
{
    [AllowAnonymous]
    public class AdminLoginController : Controller
    {
        // GET: AdminLogin
        Dbo_KutuphaneEntities db = new Dbo_KutuphaneEntities();
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Tbl_Admin a)
        {
            var bilgiler = db.Tbl_Admin.FirstOrDefault(x => x.Kullanıcı == a.Kullanıcı && x.Sifre == a.Sifre);
            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.Kullanıcı, false);
                Session["Kullanıcı"] = bilgiler.Kullanıcı.ToString();
                return RedirectToAction("Index", "Istatistik");
            }
            else
            {
                return View();
            }
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "AdminLogin");
        }
        [HttpGet]
        public ActionResult ResetPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ResetPassword(Tbl_Admin t)
        {
            if (!string.IsNullOrEmpty(t.Kullanıcı))
            {
                try
                {
                    var fromAddress = new MailAddress("admin1@gmail.com", "Admin");
                    var toAddress = new MailAddress(t.Kullanıcı);
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