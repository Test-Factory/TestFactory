using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFactory.Business.DataProviderContracts;
using TestFactory.Business.Models;
using NHibernate.Criterion;

namespace TestFactory.NHibernateDataProvider.DataProviders
{
    public class NHibernateSubjectMarkDataProvider : NHibernateDataProviderBase<SubjectMark>, ISubjectMarkDataProvider
    {
        public IList<SubjectMark> GetMarkForSubject(string subjectId)
        {
            return Execute(session =>
                {
                    return session.CreateCriteria(typeof(SubjectMark))
                        .Add(Restrictions.Eq("SubjectId", subjectId))
                        .List<SubjectMark>();
                });
        }

    }
}
