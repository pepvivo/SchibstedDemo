using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Managers;
using WebApplication.Models;
using WebApplication.Modules;
//using System.Web..AspNet.Identity.Owin

namespace WebApplication.Controllers
{
    public class LoginController : Controller
    {
        private UserManager _userManager;
        private SessionManager _sessionManager;

        public LoginController()
        {
            _userManager = new UserManager();
            _sessionManager = SessionManager.Instance;
        }

        /// <summary>
        /// Constructor for testing purposes
        /// </summary>
        /// <param name="userManager"></param>
        public LoginController(UserManager userManager, SessionManager sessionManager)
        {
            _userManager = userManager;
            _sessionManager = sessionManager;
        }

        public ActionResult Login(string ReturnPage)
        {
            ViewData["ReturnPage"] = ReturnPage;
            return View("Login");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginData loginData)
        {
            if (ModelState.IsValid)
            {

                User user = _userManager.Get(loginData.UserName);
                if (user != null && user.IsPasswordOk(loginData.Password))
                {
                    _sessionManager.AddUserToSession(user);

                    if (!string.IsNullOrEmpty(loginData.ReturnPage))
                    {
                        return Redirect(loginData.ReturnPage);
                    }
                    else
                        return Redirect("/Welcome.html");
                }

                ViewData["Message"] = Resources.AppMessages.bad_user_or_password;
                return View("Login");
            }
            return View(loginData);
        }

        public ActionResult NotAuthorized()
        {
            return View();

        }

    }
}