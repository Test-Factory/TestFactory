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
    public class MarkMap : ClassMap<Mark>
    {
        public MarkMap()
        {
            Id(x => x.Id);

            Map(x => x.StudentId).Not.Nullable();

            Map(x => x.CategoryId);

            Map(x => x.Value);
        }
    }
}
