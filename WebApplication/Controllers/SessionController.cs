using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Managers;

namespace WebApplication.Controllers
{
    public class SessionController : Controller
    {
        // GET: Session
        public ActionResult CloseSession()
        {
            SessionManager.Instance.Abandon();
            return View();
        }
    }
}