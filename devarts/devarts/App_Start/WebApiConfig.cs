using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace devarts
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {            
            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "LitterApi",
            //    routeTemplate: "api/{controller}/{action}/{id}",
            //    defaults: new { litterid = RouteParameter.Optional }
            //);

            config.Routes.MapHttpRoute(
                name: "AddPuppyAPI",
                routeTemplate: "api/{controller}/{action}/{breedLink}/{litterLink}/{breedId}/{dogId}/{litterId}/{dogLink}/{puppyLink}",
                defaults: new
                {
                    breedLink = RouteParameter.Optional,
                    litterLink = RouteParameter.Optional,
                    breedId = RouteParameter.Optional,
                    dogId = RouteParameter.Optional,
                    litterId = RouteParameter.Optional,
                    dogLink = RouteParameter.Optional,
                    puppyLink = RouteParameter.Optional
                }
            );

            config.Routes.MapHttpRoute(
                name: "AddLitter",
                routeTemplate: "api/{controller}/{action}/{breedLink}/{litterLink}/{breedId}/{dogId}/{litterId}/{dogLink}",
                defaults: new {
                    breedLink = RouteParameter.Optional,
                    litterLink = RouteParameter.Optional,
                    breedId = RouteParameter.Optional,
                    dogId = RouteParameter.Optional,
                    litterId = RouteParameter.Optional,
                    dogLink = RouteParameter.Optional
                }
            );

            // id nie jest nazwą uploadowanego pliku tylko zwyczajnie parametrem id
            config.Routes.MapHttpRoute(
                name: "AddDog",
                routeTemplate: "api/upload/dog/{breedLink}/{dogLink}/{dogId}",
                defaults: new { breedLink = RouteParameter.Optional, dogLink = RouteParameter.Optional, dogId = RouteParameter.Optional }
            );

            // config.Routes.MapHttpRoute(
            //    name: "AddLitter",
            //    routeTemplate: "api/{controller}/{action}/{directory}/{id}/{dogId}",
            //    defaults: new { directory = RouteParameter.Optional, id = RouteParameter.Optional, dogId = RouteParameter.Optional }
            //);

            // ta opcja zakłada, że w kontrolerze jest tylko jedna akcja - trochę słabo
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/upload/post/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
