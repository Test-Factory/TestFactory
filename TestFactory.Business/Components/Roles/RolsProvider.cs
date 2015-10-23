using System.Web.Security;
using TestFactory.Business.DataProviderContracts;
using TestFactory.Business.Models;

namespace TestFactory.Business.Components.Rols
{
    public class RolsProvider : SqlRoleProvider
    {
        IRoleDataProvider dataProvider;

        public RolsProvider(IRoleDataProvider provider)
        {
            this.dataProvider = provider;
        }

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
