using TestFactory.Business.Models;

namespace TestFactory.Business.DataProviderContracts
{
    public interface IUserDataProvider  : IDataProvider<User>
    {
        User GetByEmail(string email);
    }
}
