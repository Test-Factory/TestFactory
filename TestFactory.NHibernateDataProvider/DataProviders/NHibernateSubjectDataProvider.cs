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
        public Subject GetByName(string name)
        {
            return Execute(session =>
            {
                return session
                    .CreateCriteria<Subject>()
                    .Add(Restrictions.Eq("Name", name))
                    .UniqueResult<Subject>();
            });
        }
    }
}
