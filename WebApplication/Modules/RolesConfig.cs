using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Modules
{
    public static class RolesConfig
    {

        /// <summary>
        /// Role for administrators
        /// </summary>
        public const string ROLE_ADMIN = "Admin";

        /// <summary>
        /// Role for Page1
        /// </summary>
        public const string ROLE_PAGE1 = "Admin,Page1";

        /// <summary>
        /// Role for Page2
        /// </summary>
        public const string ROLE_PAGE2 = "Admin,Page2";

        /// <summary>
        /// Role for Page3
        /// </summary>
        public const string ROLE_PAGE3 = "Admin,Page3";
    }
}