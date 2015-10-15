using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFactory.Business.DataProviderContracts;
using TestFactory.Business.Models;

namespace TestFactory.Business.Components.Managers
{
   public class GroupForUserManager : BaseManager<GroupForUser, IGroupForUserDataProvider>
    {
        public GroupForUserManager(IGroupForUserDataProvider provider) : base(provider) { }
    }
}
