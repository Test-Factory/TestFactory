using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Criterion;
using NHibernate.Linq;
using TestFactory.Business.DataProviderContracts;
using TestFactory.Business.Models;

namespace TestFactory.NHibernateDataProvider.DataProviders
{
    public class NHibernateFrequencyMarkForFacultyByCategoryDataProvider: NHibernateDataProviderBaseForView<FrequencyMarkForFacultyByCategory>, IFrequencyMarkForFacultyByCategoryDataProvider
    {
        public IList<FrequencyMarkForFacultyByCategory> GetMarksForFaculty(string facultyId)
        {
            return Execute(session =>
            {
                return session
                    .CreateCriteria(typeof(FrequencyMarkForFacultyByCategory))
                    .Add(Restrictions.Eq("FacultyId", facultyId))
                    .List<FrequencyMarkForFacultyByCategory>();
            });
        }
    }
}
