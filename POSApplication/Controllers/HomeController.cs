using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace POSApplication.Controllers
{


    [Authorize]
    public class HomeController : Controller
    {
      
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        public ActionResult SignOut()
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