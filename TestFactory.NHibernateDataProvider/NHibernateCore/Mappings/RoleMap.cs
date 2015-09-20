using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using TestFactory.Business.Models;

namespace TestFactory.NHibernateDataProvider.NHibernateCore.Mappings
{
    class RoleMap : ClassMap<Role>
    {
        public RoleMap()
        {
            Id(x => x.Id);

            Map(x => x.Name);
        }
    }
}
