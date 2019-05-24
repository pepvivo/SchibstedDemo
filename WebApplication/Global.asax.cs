using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Http;
using WebApplication.Models;
using WebApplication.Managers;

namespace WebApplication
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Código que se ejecuta al iniciar la aplicación
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            
            //ModelBinders.Binders.DefaultBinder = new CustomModelBinder() ;
            //ModelBinders.Binders.Add(typeof(UserViewModel), new CustomModelBinder() );
        }

        void Session_Start(object sender, EventArgs e)
        {
            SessionManager.Instance.Init();
        }

        void Session_End(object sender, EventArgs e)
        {
            //SessionManager.Instance.Abandon();
        }

    }
}