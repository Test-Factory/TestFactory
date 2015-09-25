using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernateDataProviders.NHibernateCore;
using TestFactory.Business.DataProviderContracts;
using TestFactory.Business.Models;

namespace TestFactory.NHibernateDataProvider.DataProviders
{
    public class NHibernateCategoryDataProvider :  NHibernateDataProviderBase<Category>, ICategoryDataProvider
    {
    }
}
