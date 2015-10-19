using TestFactory.Business.Models;
using System.Collections.Generic;

namespace TestFactory.Business.DataProviderContracts
{
    public interface IGroupDataProvider : IDataProvider<Group>
    {
        int GetCount(string gropId);

        IList<GroupForUser> GetListForUser(string userId);

        Group GetByShortName(string shortName);
    }
}
