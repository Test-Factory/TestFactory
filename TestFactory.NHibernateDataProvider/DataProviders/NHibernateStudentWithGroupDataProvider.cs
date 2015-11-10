using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                IList<StudentWithGroup> students = session
                                     .Query<StudentWithGroup>()
                                     .Where(s => s.GroupId == groupId)
                                     .ToList<StudentWithGroup>();
                return students;
            });
        }
    }
}
