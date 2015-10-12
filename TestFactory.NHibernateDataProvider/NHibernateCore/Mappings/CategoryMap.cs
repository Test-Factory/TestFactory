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
    public class CategoryMap : ClassMap<Category>
    {
        public CategoryMap()
        {
            Id(x => x.Id);

            Map(x => x.Name);

            Map(x => x.Code);

            Map(x => x.ACloseTypes);

            Map(x => x.Appreciate);

            Map(x => x.Details);

            Map(x => x.Features);

            Map(x => x.Likes);

            Map(x => x.OppositeType);

            Map(x => x.PreferredAreasOfActivity);


        }
    }
}
