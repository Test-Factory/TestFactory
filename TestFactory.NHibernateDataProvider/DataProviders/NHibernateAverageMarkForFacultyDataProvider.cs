using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Linq;
using TestFactory.Business.Components.Managers;
using TestFactory.Business.DataProviderContracts;
using TestFactory.Business.Models;

namespace TestFactory.NHibernateDataProvider.DataProviders
{
    public class NHibernateAverageMarkForFacultyDataProvider: NHibernateDataProviderBaseForView<AverageMarkForFaculty>, IAverageMarkForFacultyDataProvider
    {
        public IList<AverageMarkForFaculty> GetMarksForFaculty(string faculty)
        {
            return Execute(session =>
            {
                IList<AverageMarkForFaculty> averageMarks = session
                                     .Query<AverageMarkForFaculty>()
                                     .Where(s => s.Faculty == faculty)
                                     .ToList<AverageMarkForFaculty>();
                return averageMarks;
            });
            return null;
        }
    }
}
