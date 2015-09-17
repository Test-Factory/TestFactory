using System;
using System.Collections.Generic;
using NHibernate.Criterion;
using TestFactory.Business.DataProviderContracts;
using TestFactory.Business.Models;

namespace TestFactory.NHibernateDataProvider.DataProviders
{
    public class NHibernateStudentDataProvider: NHibernateDataProviderBase<Student>, IStudentDataProvider
    {
        public IList<Student> GetByGroupId(string groupId)
        {
            return Execute(session => session
                .CreateCriteria<Student>()
                .Add(Restrictions.Eq("GroupId", groupId))
                .List<Student>()); 
        }
    }
}
