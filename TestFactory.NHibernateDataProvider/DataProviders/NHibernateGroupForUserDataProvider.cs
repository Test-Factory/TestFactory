using System.Collections.Generic;
using System.Linq;
using NHibernate.Linq;
using TestFactory.Business.DataProviderContracts;
using TestFactory.Business.Models;

namespace TestFactory.NHibernateDataProvider.DataProviders
{
    public class NHibernateGroupForUserDataProvider : NHibernateDataProviderBase<GroupForUser>, IGroupForUserDataProvider
    {
        public IList<GroupForUser> GetIdByGroupId(string id)
        {
            return Execute(session => session.Query<GroupForUser>()
                .Where(s => s.GroupId == id)
                .ToList<GroupForUser>());
        }
    }
}
