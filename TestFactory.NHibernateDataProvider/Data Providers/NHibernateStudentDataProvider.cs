using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernateDataProviders.Data_Providers;
using TestFactory.Business.Data_Provider_Contracts;
using TestFactory.Business.Models;

namespace TestFactory.NHibernateDataProvider.Data_Providers
{
    public class NHibernateStudentDataProvider: NHibernateDataProviderBase<Student>, IStudentDataProvider
    {
    }
}
