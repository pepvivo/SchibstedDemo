using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Modules;

namespace WebApplication.Controllers
{
    [AuthorizedRoles(Roles = RolesConfig.ROLE_PAGE1)]
    public class Page1Controller : SessionBaseController
    {
        // GET: Page1
        public ActionResult Index()
        {
            return View("Index");
        }
    }
}