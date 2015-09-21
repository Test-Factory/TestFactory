using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFactory.Business.Models;

namespace TestFactory.Business.DataProviderContracts
{
    public interface IRoleDataProvider : IDataProvider<Role>
    {
        Role GetRole(string rolename);

        bool IsUserInRole(string username, string rolename);
    }
}
