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

        public bool HasAccessToGroup(string faculty, string groupId)
        {
            var ListGroup = GetListForFaculty(faculty);

            foreach (var item in ListGroup)
            {
                if (item.Id == groupId)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsIncludeHTMLAttributes(Group group)
        {
            string regEx = @"<\\?([A-Z][A-Z0-9]*)\b[^>]*>";
            System.Text.RegularExpressions.Regex regular = new System.Text.RegularExpressions.Regex(regEx, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            var matches = regular.Match(group.FullName + group.ShortName);
            return matches.Success;
        }

        public bool GroupIsAlreadyExist(string shortName) 
        {
            return provider.GetByShortName(shortName) != null ? true : false;
        }

        public IList<Group> GetListForFaculty(string faculty)
        {
            return provider.GetListForFaculty(faculty);
        }
    }
}

