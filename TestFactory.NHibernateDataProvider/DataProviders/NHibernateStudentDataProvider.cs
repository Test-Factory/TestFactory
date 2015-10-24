using System.Collections.Generic;
using System.Linq;
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
                IList<Student> students = session
                                     .Query<Student>()
                                     .Where(s => s.GroupId == groupId)
                                     .ToList<Student>();
                return students;
            });
        }
    }
}
