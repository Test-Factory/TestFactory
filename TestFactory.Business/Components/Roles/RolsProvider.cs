using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using System.Configuration.Provider;
using System.Web.Mvc;
using TestFactory.Business.DataProviderContracts;
using TestFactory.Business.Models;
using System.Collections.Specialized;
using TestFactory.Business.Components.Managers;

namespace TestFactory.Business.Components.Rols
{
    public class RolsProvider : RoleProvider
    {
        IRoleDataProvider dataProvider;

        public override void Initialize(string name, NameValueCollection config)
        {

            dataProvider = (IRoleDataProvider)DependencyResolver.Current.GetService(typeof(IRoleDataProvider));
            base.Initialize(name, config);
        }

        public override bool IsUserInRole(string username, string rolename)
        {
            return dataProvider.IsUserInRole(username, rolename);
        }

        private Role GetRole(string rolename)
        {
            return dataProvider.GetRole(rolename);
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName { get; set; }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
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

        public override string[] GetRolesForUser(string username)
        {
            return new List<string>() { "Filler" }.ToArray();
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
