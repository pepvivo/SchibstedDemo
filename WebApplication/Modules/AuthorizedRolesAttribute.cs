using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using WebApplication.Managers;

namespace WebApplication.Modules
{
    public class AuthorizedRolesAttribute : AuthorizeAttribute
    {
        private SessionManager _sessionManager;
        private HttpResponseBase _httpResponseBase;

        /// <summary>
        /// Default constructor
        /// </summary>
        public AuthorizedRolesAttribute()
        {
            _sessionManager = SessionManager.Instance;
            _httpResponseBase = null;
        }

        /// <summary>
        /// Constructor with dependence injection class.
        /// Usefull for testing purposes
        /// </summary>
        /// <param name="sessionManager"></param>
        public AuthorizedRolesAttribute(SessionManager sessionManager, HttpResponseBase httpResponseBase)
        {
            _sessionManager = sessionManager;
            _httpResponseBase = httpResponseBase;
        }

        private HttpResponseBase GetResponse(AuthorizationContext filterContext)
        {
            if (_httpResponseBase != null)
                return _httpResponseBase;

            return filterContext.HttpContext.Response;
        }

        /// <summary>
        /// Unauthorization handler
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            HttpResponseBase httpResponse = GetResponse(filterContext);

            httpResponse.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
            //httpResponse.Status = "Forbidden";
            httpResponse.StatusDescription = "Unauthorized Page";
            httpResponse.End();
            httpResponse.Close();
            
            //filterContext.Result = new HttpUnauthorizedResult();

            base.HandleUnauthorizedRequest(filterContext);
        }

        /*
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            try
            {
                base.OnAuthorization(filterContext);
            }
            catch(Exception )
            {
                //log
            }
        }
        */

        /// <summary>
        /// Checks if user has role permission for controllers with this attribute
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }

            if (_sessionManager.IsUserAutentificated)
            {
                foreach( var role in _sessionManager.CurrentUser.Roles)
                {
                    if (Roles.Contains(role.ToString()))
                        return true;
                }
                return false;

            }

            // Important desviation of its regular use
            // After: The responsability of authentification will be by SessionBaseController
            return true;
        }

    }
}