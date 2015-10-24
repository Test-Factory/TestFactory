﻿using TestFactory.Business.DataProviderContracts;
using TestFactory.Business.Models;

namespace TestFactory.NHibernateDataProvider.DataProviders
{
    public class NHibernateCategoryDataProvider :  NHibernateDataProviderBase<Category>, ICategoryDataProvider
    {
    }
}
