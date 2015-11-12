using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Criterion;
using NHibernate.Linq;
using TestFactory.Business.DataProviderContracts;
using TestFactory.Business.Models;

namespace TestFactory.NHibernateDataProvider.DataProviders
{
    public class NHibernateStudentWithGroupDataProvider: NHibernateDataProviderBaseForView<StudentWithGroup>, IStudentWithGroupDataProvider
    {
        public IList<StudentWithGroup> GetByGroupId(string groupId)
        {
            return Execute(session =>
            {
                return session
                    .CreateCriteria(typeof(StudentWithGroup))
                    .Add(Restrictions.Eq("GroupId", groupId))
                    .List<StudentWithGroup>();
            });
        }
    }
}
