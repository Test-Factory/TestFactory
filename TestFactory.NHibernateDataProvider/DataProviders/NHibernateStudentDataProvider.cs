using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using NHibernateDataProviders.NHibernateCore;
using TestFactory.Business.DataProviderContracts;
using TestFactory.Business.Models;

namespace TestFactory.NHibernateDataProvider.DataProviders
{
    public class NHibernateStudentDataProvider: NHibernateDataProviderBase<Student>, IStudentDataProvider
    {
        public IList<Student> GetByGroupId(string groupId)
        {
            return Execute(session => session.Query<Student>()
                .Where(s => s.Group.Id == groupId)
                .ToList<Student>()); 
        }
    }
}
