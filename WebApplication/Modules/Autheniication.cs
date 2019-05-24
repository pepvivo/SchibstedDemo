using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace WebApplication.Modules
{
    public class Autheniication
    {
        public virtual bool IsAuthentificated(IPrincipal user)
        {
            return user != null && user.Identity  != null 
                ? user.Identity.IsAuthenticated 
                : false;
        }
    }
}