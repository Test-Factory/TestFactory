using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFactory.Business.Models;
using TestFactory.Business.Data_Provider_Contracts;
using NHibernate.Criterion;
using NHibernateDataProviders.Data_Providers;

namespace TestFactory.NHibernateDataProvider.Data_Providers
{
    public class NHibernateGroupDataProvider : NHibernateDataProviderBase<Group>, IGroupDataProvider
    {
 
    }
}
