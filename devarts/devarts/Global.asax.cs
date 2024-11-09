using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using App_Start;
using devarts.App_Start;
using devarts.Helpers;

namespace devarts
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();            
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            InitializeMembership.SeedMembership();
            BundleTable.EnableOptimizations = false;
        }

        private void Application_BeginRequest(Object source, EventArgs e)
        {
            string actualIP = HttpContext.Current.Request.UserHostAddress;
            string url = HttpContext.Current.Request.Url.AbsolutePath;
            int deviceType = 0;

            deviceType = (DetectBrowser.IsMobileBrowser(Request)) ? 1 : 0; // 0 - pc, 1 - mobilny

            InitializeStatistics istat = new InitializeStatistics();

            /// LOGOWANIE
            //FakeLogged.LoggedAsAdmin();

            try
            {
                if (!Request.Cookies.AllKeys.Contains("StatCook"))
                {
                    // dodawanie ciastka mówiącego, że zalogowano się raz w celu pobrania IP
                    HttpCookie cookie = new HttpCookie("StatCook");
                    cookie.Value = DateTime.Now.ToString();
                    HttpContext.Current.Response.Cookies.Add(cookie);

                    istat.AddUser(actualIP, deviceType);
                }
            }
            catch { }

            try
            {
                if (!url.Contains("browserLink") && !url.Contains("bundle") && !url.Contains("Ajax") && !url.Contains("css") && !(url == "/"))
                {
                    istat.AddTrace(actualIP, url, deviceType);
                }
            }
            catch { }
        }
    }
}
