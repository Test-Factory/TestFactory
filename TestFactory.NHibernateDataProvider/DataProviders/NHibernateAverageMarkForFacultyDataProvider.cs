using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFactory.Business.Components.Managers;
using TestFactory.Business.DataProviderContracts;

namespace TestFactory.NHibernateDataProvider.DataProviders
{
    public class NHibernateAverageMarkForFacultyDataProvider: NHibernateDataProviderBaseForView<AverageMarkForFaculty>, IAverageMarkForFacultyDataProvider
    {
    }
}
