using TestFactory.Business.Models;

namespace TestFactory.Business.DataProviderContracts
{
    public interface IGroupDataProvider : IDataProvider<Group>
    {
        int GetCount(string gropId);
    }
}
