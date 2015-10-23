
namespace TestFactory.Business.DataProviderContracts
{
    public interface IRoleDataProvider: IDataProvider<Models.Role>
    {
        Models.Role GetRole(string rolename);

        bool IsUserInRole(string username, string rolename);
    }
}
