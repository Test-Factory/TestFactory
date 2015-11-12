using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using TestFactory.Business.Models;

namespace TestFactory.NHibernateDataProvider.NHibernateCore.Mappings
{
    public class FrequencyMarkForFacultyByCategoryMap: ClassMap<FrequencyMarkForFacultyByCategory>
     {
        public FrequencyMarkForFacultyByCategoryMap()
        {
            ReadOnly();

            CompositeId()
                .KeyProperty(x => x.CategoryId)
                .KeyProperty(x => x.Value); 

            Map(x => x.FacultyId);

            Map(x => x.Code);

            Map(x => x.CategoryId);

            Map(x => x.Value);

            Map(x => x.Count);
        }
    }
}
