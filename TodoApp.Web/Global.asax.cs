﻿using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Newtonsoft.Json.Serialization;

namespace TodoApp.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            ConfigureWebApi();
            ConfigureMvcRoutes(RouteTable.Routes);
        }

        private static void ConfigureWebApi()
        {
            ConfigureWebApiRoutes(GlobalConfiguration.Configuration.Routes);

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }

        private static void ConfigureWebApiRoutes(HttpRouteCollection routes)
        {
            routes.MapHttpRoute(
                "API Default",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional }
            );
        }

        private static void ConfigureMvcRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new {controller = "Home", action = "Index", id = UrlParameter.Optional});

            routes.MapRoute(
                "Index",
                "{controller}/{id}",
                new {controller = "Home", action = "Index"});
        }
    }
}
