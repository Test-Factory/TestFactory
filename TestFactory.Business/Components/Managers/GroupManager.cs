using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFactory.Business.DataProviderContracts;
using TestFactory.Business.Models;

namespace TestFactory.Business.Components.Managers
{
    public class GroupManager : BaseManager<Group, IGroupDataProvider>
    {
        public GroupManager(IGroupDataProvider provider) : base(provider) { }

        public int GetCount(string gropId)
        {
            return provider.GetCount(gropId);
        }

        public IList<Group> GetListForUser(string userId)
        {
            var UserGroupsId = provider.GetListForUser(userId);
            var ListGroup = new List<Group>();
            foreach (var gr in UserGroupsId)
            {
                var groupById = provider.GetById(gr.GroupId);
                ListGroup.Add(groupById);
            }
            return ListGroup;
        }
    }
}

