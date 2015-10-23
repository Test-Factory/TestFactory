using System.Collections.Generic;
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

        private IList<Group> GetUsersGroups(IList<GroupForUser> UserGroupsId) 
        {
            var ListGroup = new List<Group>();
            foreach (var gr in UserGroupsId)
            {
                var groupById = provider.GetById(gr.GroupId);
                ListGroup.Add(groupById);
            }
            return ListGroup;
        }

        public IList<Group> GetListForUser(string userId)
        {
            var UserGroupsId = provider.GetListForUser(userId);
            return GetUsersGroups(UserGroupsId);
        }

        public bool HasAccessToGroup(string groupId, string userId)
        {
            var ListGroup = GetListForUser(userId);

            Group currentGroup = provider.GetById(groupId);

            foreach (var group in ListGroup) 
            {
                if (group.Id == currentGroup.Id)
                {
                    return true;
                }
            }
            return false;
        }
        public bool GroupIsAlreadyExist(string shortName) 
        {
            var a = provider.GetByShortName(shortName);

            if (a != null)
            {
                return true;
            }
            return false;
        }
    }
}

