using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using WebApplication.Models;

namespace WebApplication.Managers
{
    public class SessionManager
    {
        private const string CURRENT_USER = "CURRENT_USER";
        private static readonly object sessionLock = new object();

        private static readonly object singletonLock = new object();
        private static readonly SessionManager instance = null;


        /// <summary>
        /// Singleton instance of SessionManager Class
        /// </summary>
        public static SessionManager Instance
        {
            get
            {
                lock (singletonLock)
                {
                    return instance ?? new SessionManager();
                }
            }
        }

        public virtual User CurrentUser
        {
            get => (User)HttpContext.Current.Session[CURRENT_USER];
            set
            {
                lock (sessionLock)
                {
                    HttpContext.Current.Session[CURRENT_USER] = value;
                }
            }
        }

        public void Init()
        {
            HttpContext.Current.Session.Add(CURRENT_USER, null);
        }

        public bool IsUserAutentificated
        {
            get
            {
                return CurrentUser != null;
                //return HttpContext.Current.User != null && HttpContext.Current.User.Identity.IsAuthenticated && CurrentUser != null;
            }
        }

        public void Abandon()
        {
            FormsAuthentication.SignOut();
            if (HttpContext.Current != null)
            {
                HttpContext.Current.Session.RemoveAll();
                HttpContext.Current.Session.Abandon();
            }
        }

        public string GetIdentityUserName()
        {
            string user_id = string.Empty;
            if (HttpContext.Current.User != null && HttpContext.Current.User.Identity is FormsIdentity)
            {
                FormsAuthenticationTicket ticket = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket;
                if (ticket != null)
                {
                    user_id = ticket.UserData;
                }
            }
            return user_id;
        }

        public virtual void AddUserToSession(User user)
        {
            CurrentUser = user;

            var ExpirationMinutes = Convert.ToInt16(ConfigurationManager.AppSettings["SessionTimeOut"].ToString());

            DateTime expirationTime = DateTime.Now.AddMinutes(ExpirationMinutes);
            var ticket = new FormsAuthenticationTicket(1, user.UserName, DateTime.Now, expirationTime, true, user.GetStringRoles());
            var encryptedTicket = FormsAuthentication.Encrypt(ticket);
            var isSsl = HttpContext.Current.Request.IsSecureConnection;

            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
            {
                HttpOnly = true,
                Secure = isSsl,
            };

            cookie.Expires = expirationTime;

            //FormsAuthentication.SetAuthCookie(user.UserName, true);
            HttpContext.Current.Session.Timeout = ExpirationMinutes;

            HttpContext.Current.Response.Cookies.Set(cookie);

        }
    }
}