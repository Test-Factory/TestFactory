using System.Collections.Generic;
using System.Linq;
using NHibernate.Criterion;
using NHibernate.Linq;
using TestFactory.Business.DataProviderContracts;
using TestFactory.Business.Models;

namespace TestFactory.NHibernateDataProvider.DataProviders
{
    public class NHibernateMarkDataProvider : NHibernateDataProviderBase<Mark>, IMarkDataProvider
    {
        public IList<Mark> GetByStudentId(string studentId)
        {
            return Execute(session =>
            {
                return session
                    .CreateCriteria(typeof(Mark))
                    .Add(Restrictions.Eq("StudentId", studentId))
                    .List<Mark>();
            });
        }

        public IList<Mark> GetMarksByCategoryId(string categoryId)
        {
            return Execute(session =>
            {
                return session
                    .CreateCriteria(typeof(Mark))
                    .Add(Restrictions.Eq("CategoryId", categoryId))
                    .List<Mark>();
            });
        }

        public int CountMarksForCategory(string categoryId)
        {
            return Execute(session =>
            {
                var criteria = session
                    .CreateCriteria(typeof(Mark))
                    .Add(Restrictions.Eq("CategoryId", categoryId))
                    .SetProjection(Projections.CountDistinct("Id"));
                return (int)criteria.UniqueResult();
            });
        }
    }
}
