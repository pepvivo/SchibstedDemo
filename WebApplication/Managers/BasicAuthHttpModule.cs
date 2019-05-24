
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Security;
using WebApplication.Models;
using WebApplication.Modules;

namespace WebApplication.Managers
{
    /// <summary>
    /// BasicAuthHttpModule Class
    /// </summary>
    public class BasicAuthHttpModule : IHttpModule
    {
        private const string Realm = "SchibstedRealm";
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Initialization of BasicAuthHttpModule class
        /// </summary>
        /// <param name="context"></param>
        public void Init(HttpApplication context)
        {
            // Register event handlers
            context.AuthenticateRequest += OnApplicationAuthenticateRequest;
            context.EndRequest += OnApplicationEndRequest;
        }


        /// <summary>
        /// Establish IPrincipal user identity
        /// </summary>
        /// <param name="principal"></param>
        private static void SetPrincipal(IPrincipal principal)
        {
            Thread.CurrentPrincipal = principal;
            if (HttpContext.Current != null)
            {
                HttpContext.Current.User = principal;

            }

            if (principal != null)
                log.Debug(string.Format("User logged {0} is authenticated {1}", principal.Identity.Name, principal.Identity.IsAuthenticated));

        }

        /// <summary>
        /// Check user password process
        /// </summary>
        /// <param name="user">User instance</param>
        /// <param name="password">PAssword to check</param>
        /// <returns>true if password is OK, false if no</returns>
        private static bool CheckPassword(User user, string password)
        {
            if (user == null)
                return false;

            return user.IsPasswordOk(password);
        }

        /// <summary>
        /// User Authentication process
        /// </summary>
        /// <param name="credentials"></param>
        private static void AuthenticateUser(string credentials)
        {
            log.Debug("begin");
            try
            {

                var encoding = Encoding.GetEncoding("iso-8859-1");
                credentials = encoding.GetString(Convert.FromBase64String(credentials));

                int separator = credentials.IndexOf(':');
                string name = credentials.Substring(0, separator);
                string password = credentials.Substring(separator + 1);

                var user = new UserManager().Get(name);

                if (user == null)
                {
                    // Invalid username 
                    SetPrincipal(null);
                    HttpContext.Current.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    log.Warn(string.Format("Invalid username: {0}",name));
                    return;

                }

                if (CheckPassword(user, password))
                {
                    var identity = new GenericIdentity(name);
                    SetPrincipal(new GenericPrincipal(identity, user.GetStringArrayRoles() ));
                }
                else
                {
                    // Invalid username or password.
                    HttpContext.Current.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    log.Warn(string.Format("Invalid username: {0} or password", name));
                }
            }
            catch (FormatException ex)
            {
                HttpContext.Current.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                log.Warn("Credentials were not formatted correctly", ex);
            }

            log.Debug("end");
        }

        /// <summary>
        /// Function delegates of AuthenticateRequest Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnApplicationAuthenticateRequest(object sender, EventArgs e)
        {

            var request = HttpContext.Current.Request;
            var authHeader = request.Headers["Authorization"];
            if (authHeader != null)
            {
                var authHeaderVal = AuthenticationHeaderValue.Parse(authHeader);

                if (authHeaderVal.Scheme.Equals("basic", StringComparison.OrdinalIgnoreCase) &&
                    authHeaderVal.Parameter != null)
                {
                    AuthenticateUser(authHeaderVal.Parameter);
                }
            }
        }

        /// <summary>
        /// Function delegates of EndRequest Event.
        /// If the request was unauthorized, add the WWW-Authenticate header 
        /// to the response.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnApplicationEndRequest(object sender, EventArgs e)
        {

            var response = HttpContext.Current.Response;
            if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
            {
                response.Headers.Add("WWW-Authenticate",
                    string.Format("Basic realm=\"{0}\"", Realm));
            }

        }

        /// <summary>
        /// Dispose class
        /// </summary>
        public void Dispose()
        {            

        }
    }

}