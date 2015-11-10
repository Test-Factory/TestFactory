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
            return Execute(session => session.Query<Mark>()
                .Where(s => s.StudentId == studentId)
                .ToList<Mark>());
        }

        public IEnumerable<Mark> GetMarksByCategoryId(string id)
        {
            return Execute(session => session.Query<Mark>()
                .Where(s => s.CategoryId == id)
                .ToList<Mark>());
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
