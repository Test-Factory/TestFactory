using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Criterion;
using TestFactory.Business.DataProviderContracts;
using TestFactory.Business.Models;

namespace TestFactory.NHibernateDataProvider.DataProviders
{
    public class NHibernateSubjectDataProvider    : NHibernateDataProviderBase<Subject>, ISubjectDataProvider
    {
        public IList<Subject> GetForFaculty(string facultyId)
        {
            return Execute(session =>
            {
                return session
                    .CreateCriteria(typeof(Subject))
                    .Add(Restrictions.Eq("FacultyId", facultyId))
                    .List<Subject>();
            });
        }
        public Subject GetByShortName(string shortName)
        {
            return Execute(session =>
            {
                return session
                    .CreateCriteria<Subject>()
                    .Add(Restrictions.Eq("ShortName", shortName))
                    .UniqueResult<Subject>();
            });
        }
        public Subject GetByLongName(string longName)
        {
            return Execute(session =>
            {
                return session
                    .CreateCriteria<Subject>()
                    .Add(Restrictions.Eq("LongName", longName))
                    .UniqueResult<Subject>();
            });
        }
    }
}
