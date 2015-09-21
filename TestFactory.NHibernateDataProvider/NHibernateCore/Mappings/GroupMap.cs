using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using TestFactory.Business.Models;
using NHibernate;

namespace NHibernateDataProviders.NHibernateCore.Mappings
{
    public class GroupMap : ClassMap<Group>
    {
        public GroupMap()
        {
            Id(x => x.Id);

            Map(x => x.FullName);

            Map(x => x.ShortName);
            
            //HasMany(x => x.Students);
            HasMany(x => x.Students).KeyColumn("GroupId").Inverse().Cascade.All();
        }
    }
}
