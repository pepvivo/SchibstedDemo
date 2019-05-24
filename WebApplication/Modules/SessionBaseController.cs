using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebApplication.Managers;
using WebApplication.Models;

namespace WebApplication.Modules
{
    public abstract class SessionBaseController : Controller
    {    
        public SessionBaseController()
        {

        }        

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            base.OnActionExecuting(filterContext);

            if (!SessionManager.Instance.IsUserAutentificated)
            {
                LoginData loginData = new LoginData()
                {
                    ReturnPage = filterContext.HttpContext.Request.Url.GetComponents(UriComponents.PathAndQuery, UriFormat.SafeUnescaped)
                };              

                filterContext.Result =  base.RedirectToAction("Login", "Login", loginData);
            }   
        }
    }
}