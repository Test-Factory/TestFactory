<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using NHibernateDataProviders.NHibernateCore;
using TestFactory.Business.DataProviderContracts;
=======
﻿using TestFactory.Business.DataProviderContracts;
>>>>>>> origin/master
using TestFactory.Business.Models;

namespace TestFactory.NHibernateDataProvider.DataProviders
{
    public class NHibernateGroupForUserDataProvider : NHibernateDataProviderBase<GroupForUser>, IGroupForUserDataProvider
    {
        public IList<GroupForUser> GetIdByGroupId(string id)
        {
            return Execute(session => session.Query<GroupForUser>()
                .Where(s => s.GroupId == id)
                .ToList<GroupForUser>());
        }
    }
}
