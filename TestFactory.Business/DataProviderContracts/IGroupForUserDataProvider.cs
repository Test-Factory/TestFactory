using TestFactory.Business.Models;

namespace TestFactory.Business.DataProviderContracts
{
    public interface IGroupForUserDataProvider : IDataProvider<GroupForUser>
    {
        IList<GroupForUser> GetIdByGroupId(string id);
    }
}
