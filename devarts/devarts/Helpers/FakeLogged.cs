using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace devarts.Helpers
{
    public static class FakeLogged
    {
        public static void LoggedAsAdmin()
        {
            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1,
            "Administrator",
            DateTime.Now,
            DateTime.Now.AddDays(90),
            true,
            string.Empty);

            // add cookie to response stream         
            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
            System.Web.HttpCookie authCookie = new System.Web.HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            if (authTicket.IsPersistent)
            {
                authCookie.Expires = authTicket.Expiration;
            }
            System.Web.HttpContext.Current.Response.Cookies.Add(authCookie);
        }        
    }
}