using System.Collections.Generic;
using System.Linq;
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
    }
}
