using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using TestFactory.Business.Models;
using NHibernate;

namespace TestFactory.NHibernateDataProvider.NHibernateCore.Mappings
{
    public class GroupForUserMap : ClassMap<GroupForUser>
    {
        public GroupForUserMap()
        {
            Id(x  => x.Id);

            Map(x => x.GroupId).Not.Nullable();

            Map(x => x.UserId).Not.Nullable();
        }
    }
}
