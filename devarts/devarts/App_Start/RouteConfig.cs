using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace devarts
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // routes.MapRoute(
            //    name: "AjaxCathegory",
            //    url: "AjaxPost/{type}",
            //    defaults: new { controller = "AjaxPost", action = "PostsAjaxList", type = UrlParameter.Optional }
            //);  
            /// FRONT-END - Widok psa
            routes.MapRoute(
            name: "PreviewLitter",
            url: "Litter/{litterLink}",
            defaults: new { controller = "Home", action = "Litter", litterLink = UrlParameter.Optional }
            );

            /// FRONT-END - Widok wszystkich szczeniąt rasy z podziałem na mioty
            routes.MapRoute(
            name: "AllPuppiesy",
            url: "Allpuppies/{breedLink}",
            defaults: new { controller = "Home", action = "AllPuppies", breedLink = UrlParameter.Optional }
            );

            /// FRONT-END - Widok szczenięcia
            routes.MapRoute(
            name: "PreviewPuppy",
            url: "Puppy/{puppyLink}",
            defaults: new { controller = "Home", action = "Puppy", puppyLink = UrlParameter.Optional }
            );

            /// FRONT-END - Widok psa
            routes.MapRoute(
            name: "PreviewDog",
            url: "Dog/{dogLink}",
            defaults: new { controller = "Home", action = "Dog", dogLink = UrlParameter.Optional }
            );

            /// FRONT-END - Widok postu
            routes.MapRoute(
            name: "PreviewPost",
            url: "Home/News/{postLink}",
            defaults: new { controller = "Home", action = "Post", postLink = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "AddPuppy",
            url: "Kennel/AddPuppy/{litterId}",
            defaults: new { controller = "Kennel", action = "AddPuppy", litterId = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "DownloadFile",
            url: "Admin/Download/{fileName}",
            defaults: new { controller = "Admin", action = "SafeDownload", fileName = UrlParameter.Optional }
            );

            /// zdefiniowana ścieżka wraz ze strukturą (URL)
            routes.MapRoute(
               name: "SinglePortfolioItem",
               url: "Portfolio/{postLink}",
               defaults: new { controller = "Portfolio", action = "Single", postLink = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "EditPost",
               url: "Admin/EditPost/{postLink}",
               defaults: new { controller = "Admin", action = "EditPost", postLink = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "HomePage",
               url: "Home/Index/{page}/{cathegory}/{content}",
               defaults: new { controller = "Home", action = "Index", page = UrlParameter.Optional, cathegory = UrlParameter.Optional, content = UrlParameter.Optional }
           );

            routes.MapRoute(
                name: "ChangeLanguage",
                url: "Home/ChangeLanguage/{lang}",
                defaults: new { controller = "Home", action = "ChangeLanguage", lang = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
