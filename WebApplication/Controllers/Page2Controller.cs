using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Modules;

namespace WebApplication.Controllers
{
    [AuthorizedRoles(Roles = RolesConfig.ROLE_PAGE2)]
    public class Page2Controller : SessionBaseController
    {
        // GET: Page2
        public ActionResult Index()
        {
            return View("Index");
        }
    }
}