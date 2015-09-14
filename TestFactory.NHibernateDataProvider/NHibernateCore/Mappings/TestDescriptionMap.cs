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
    public class TestDescriptionMap : ClassMap<TestDescription>
    {
        public TestDescriptionMap()
        {
            Id(x => x.Id);

            Map(x => x.Category);

            Map(x => x.Code);

            Map(x => x.ShortDescription);

            Map(x => x.LongDescription);
        }
    }
}
