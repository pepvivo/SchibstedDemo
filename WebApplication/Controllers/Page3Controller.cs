using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Modules;

namespace WebApplication.Controllers
{
    [AuthorizedRoles(Roles = RolesConfig.ROLE_PAGE3)]
    public class Page3Controller : SessionBaseController
    {
        // GET: Page3
        public ActionResult Index()
        {
            return View("Index");
        }
    }
}