using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using TestFactory.Business.Models;
using TestFactory.Business.Components.Managers;
using TestFactory.Business.Components;

namespace TestFactory.Components
{
    public class UserViewContext : UserContext
    {
        private bool IsPrincipalAvailable()
        {
            if (HttpContext.Current == null)
                return false;

            var principal = HttpContext.Current.User;
            if (principal == null)
                return false;

            if (principal.Identity == null)
                return false;

            return true;
        }

        private IPrincipal WebUser
        {
            get
            {
                if (!IsPrincipalAvailable())
                    return null;

                return HttpContext.Current.User;
            }
        }

        public override bool IsLogged(string role)
        {
             return IsPrincipalAvailable() && WebUser.IsInRole(role);
        }

        public override bool IsLoggedIn
        {
            get
            {
                return IsPrincipalAvailable() && WebUser.Identity.IsAuthenticated;
            }
        }

        public override User User
        {
            get
            {
                if (!IsLoggedIn)
                    return null;

                return DependencyResolver.Current.GetService<UserManager>().GetByEmail(WebUser.Identity.Name);
            }
        }
    }
}