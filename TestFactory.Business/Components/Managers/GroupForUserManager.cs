using TestFactory.Business.DataProviderContracts;
using TestFactory.Business.Models;

namespace TestFactory.Business.Components.Managers
{
   public class GroupForUserManager : BaseManager<GroupForUser, IGroupForUserDataProvider>
    {
        public GroupForUserManager(IGroupForUserDataProvider provider) : base(provider) { }

        public void DeleteByGroupId(string id)
        {
            var groupForUserList = provider.GetIdByGroupId(id);
            foreach (var el in groupForUserList)
            {
                provider.Delete(el.Id);
            }
        }
   }
}
