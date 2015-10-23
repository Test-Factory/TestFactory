using TestFactory.Business.DataProviderContracts;
using TestFactory.Business.Models;

namespace TestFactory.Business.Components.Managers
{
    public class RoleManager: BaseManager<Role, IRoleDataProvider>
    {
        public RoleManager(IRoleDataProvider provider) : base(provider) { }
    }
}

