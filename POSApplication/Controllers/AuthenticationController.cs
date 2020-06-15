﻿
using POSApplication.Helper;
using POSApplication.Helpers;
using POSApplication.Models;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace POSApplication.Controllers
{

    public class AuthenticationController : Controller
    {
       
        private SCMSContext db = new SCMSContext();
        private EncryptionDecryptionUtil encryptionDecryptionUtil = new EncryptionDecryptionUtil();
        //private int saltLength = 5;

        //// GET: Authentication
        //public ActionResult Index()
        //{
        //    return View();
        //}

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
           
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Secu_User model, string returnUrl)
        {

            var user = db.Secu_User.FirstOrDefault(x => x.UserName == model.UserName);
            
            var Message = "";
            if (user != null)
            {
                if (encryptionDecryptionUtil.VerifyPassword(user.Password, model.Password, user.Salt))
                {
                    //flagLogin = true;

                    FormsAuthentication.SetAuthCookie(model.UserName, false);

                    Session["uid"] = user.Id;
            
             
                    Session["IsAdmin"] = user.IsAdmin;
                    Session["IsSuperAdmin"] = user.IsSuperAdmin == null ? false: user.IsSuperAdmin;
                    Session["loginCounter"] = 0;

                    user.LastLoginDate = DateTime.Now;
                    db.Entry(user).State = EntityState.Modified;

                    
              

                    db.SaveChanges();

                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");

                    }

                }

               

                db.SaveChanges();

            }

            //ModelState.AddModelError("", "Invalid User/Password/Company");

            ViewBag.Message = "Invalid User Name Or Password!!";
          


         

            return View(model);



            
        }




        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            var uid = 0;
            if (Session["uid"] != null)
            {
                uid = Convert.ToInt32(Session["uid"].ToString());
            }


            //authProvider.SignOut();
            FormsAuthentication.SignOut();

            var Message = "Logged out.";
            

            Session.Abandon();
            //Success("Sign out successfully");
            return RedirectToAction("Login", "Authentication");


        }
    }
}