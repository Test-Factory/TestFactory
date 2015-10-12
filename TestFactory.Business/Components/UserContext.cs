using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Threading.Tasks;
using TestFactory.Business.Models;
using System.Web.Security;
using TestFactory.Business.Components.Managers;
using System.Web;
using System.Security.Principal;


namespace TestFactory.Business.Components
{
    public class UserContext : RoleProvider
    {
        public static UserContext Current
        {
            get
            {
                return DependencyResolver.Current.GetService<UserContext>();
            }
        }

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

        public bool IsLogged(string role)
        {
            return IsPrincipalAvailable() && WebUser.IsInRole(role);
        }

        public bool IsLoggedIn
        {
            get
            {
                return IsPrincipalAvailable() && WebUser.Identity.IsAuthenticated;
            }
        }

        public User User
        {
            get
            {
                if (!IsLoggedIn)
                    return null;

                return DependencyResolver.Current.GetService<UserManager>().GetByEmail(WebUser.Identity.Name);
            }
        }
        public override string[] GetRolesForUser(string email)
        {
            return new[] { User.Roles.Name };
        }
        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }
        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}
