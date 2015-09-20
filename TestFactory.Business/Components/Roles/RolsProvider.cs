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

namespace TestFactory.Business.Components.Rols
{
    public class RolsProvider : SqlRoleProvider
    {
        IRoleDataProvider dataProvider;

        public override bool IsUserInRole(string username, string rolename)
        {
            return dataProvider.IsUserInRole(username, rolename);
        }

        private Role GetRole(string rolename)
        {
            return dataProvider.GetRole(rolename);
        }

    }
}
