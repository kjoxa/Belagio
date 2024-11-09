using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace devarts.Helpers
{
    public static class CookiesHelper
    {
        public static HttpCookie CreateCookie(string cookieName, string cookieValue)
        {
            HttpCookie newCookie = new HttpCookie(cookieName);
            newCookie.Value = cookieValue;
            newCookie.Expires = DateTime.Now.AddHours(20);

            return newCookie;
        }
    }
}