using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Criterion;
using NHibernate.Linq;
using TestFactory.Business.Components.Managers;
using TestFactory.Business.DataProviderContracts;
using TestFactory.Business.Models;

namespace TestFactory.NHibernateDataProvider.DataProviders
{
    public class NHibernateAverageMarkForFacultyDataProvider: NHibernateDataProviderBaseForView<AverageMarkForFaculty>, IAverageMarkForFacultyDataProvider
    {
        public IList<AverageMarkForFaculty> GetMarksForFaculty(string facultyId)
        {

            return Execute(session =>
            {
                return session
                    .CreateCriteria(typeof(AverageMarkForFaculty))
                    .Add(Restrictions.Eq("FacultyId", facultyId))
                    .List<AverageMarkForFaculty>();
            });
        }
    }
}
