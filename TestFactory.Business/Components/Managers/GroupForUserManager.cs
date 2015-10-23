using TestFactory.Business.DataProviderContracts;
using TestFactory.Business.Models;

namespace TestFactory.Business.Components.Managers
{
   public class GroupForUserManager : BaseManager<GroupForUser, IGroupForUserDataProvider>
    {
        public GroupForUserManager(IGroupForUserDataProvider provider) : base(provider) { }
    }
}
