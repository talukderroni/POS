using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
namespace POSApplication
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            CultureInfo cInfo = new CultureInfo("en-GB");
            //cInfo.DateTimeFormat.ShortDatePattern = "MM/dd/yyyy";
            cInfo.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            cInfo.DateTimeFormat.DateSeparator = "/";
            Thread.CurrentThread.CurrentCulture = cInfo;
            Thread.CurrentThread.CurrentUICulture = cInfo;
        }




        protected void Session_Start(object src, EventArgs e)
        {
            if (Context.Session != null)
            {
                if (Context.Session.IsNewSession)
                {
                    var sessionCookie = Context.Session["uid"];

                    if ((sessionCookie == null))
                    {
                        FormsAuthentication.SignOut();
                        //HttpContext.Current.Response.Redirect("~/Account/Login");

                        HttpContext.Current.Response.Redirect("~/Authentication/Login");
                    }
                    else
                    {

                    }
                }
            }
        }
    }
}
