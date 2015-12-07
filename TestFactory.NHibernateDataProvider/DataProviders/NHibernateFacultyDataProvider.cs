using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFactory.Business.DataProviderContracts;
using TestFactory.Business.Models;

namespace TestFactory.NHibernateDataProvider.DataProviders
{
    public class NHibernateFacultyDataProvider : NHibernateDataProviderBase<Faculty>, IFacultyDataProvider
   {
        public IList<Faculty> GetByName(string name)
        {
            return Execute(session =>
            {
                return session
                    .CreateCriteria(typeof(Faculty))
                    .Add(Restrictions.Eq("Name", name))
                    .List<Faculty>();
            });
        }

    }
}
