using TestFactory.Business.DataProviderContracts;
using NHibernate.Criterion;
using TestFactory.Business.Models;

namespace TestFactory.NHibernateDataProvider.DataProviders
{
    public class NHibernateGroupDataProvider : NHibernateDataProviderBase<Group>, IGroupDataProvider
    {
        public int GetCount(string gropId)
        {
            return Execute(session =>
            {
                var criteria = session.CreateCriteria(typeof(Student))
                    .Add(Restrictions.Eq("GroupId", gropId))
                    .SetProjection(Projections.CountDistinct("Id"));
                return (int)criteria.UniqueResult();
            });
        }
 
    }
}
