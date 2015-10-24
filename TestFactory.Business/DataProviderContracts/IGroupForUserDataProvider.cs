using TestFactory.Business.Models;
using System.Collections.Generic;

namespace TestFactory.Business.DataProviderContracts
{
    public interface IGroupForUserDataProvider : IDataProvider<GroupForUser>
    {
        IList<GroupForUser> GetIdByGroupId(string id);
    }
}
