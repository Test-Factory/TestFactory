using System.Collections.Generic;
using System.Linq;
using NHibernate.Criterion;
using NHibernate.Linq;
using TestFactory.Business.DataProviderContracts;
using TestFactory.Business.Models;

namespace TestFactory.NHibernateDataProvider.DataProviders
{
    public class NHibernateStudentDataProvider: NHibernateDataProviderBase<Student>, IStudentDataProvider
    {
        public IList<Student> GetByGroupId(string groupId)
        {
            return Execute(session =>
            {
                return session
                    .CreateCriteria(typeof(Student))
                    .Add(Restrictions.Eq("GroupId", groupId))
                    .List<Student>();
            });
        }
    }
}
