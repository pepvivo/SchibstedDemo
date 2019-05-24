﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebApplication.Modules;

namespace WebApplication
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuración y servicios de API web

            // Rutas de API web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "UserApi",
                routeTemplate: "api/Users/{id}",
                defaults: new { controller = "Users", action = "Get", id = RouteParameter.Optional }
            );



        }
    }
}
