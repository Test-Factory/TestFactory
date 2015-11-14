using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using TestFactory.Business.Models;

namespace TestFactory.NHibernateDataProvider.NHibernateCore.Mappings
{
    public class SubjectWithGroupMap : ClassMap<SubjectWithGroup>
    {
        public SubjectWithGroupMap()
        {
            ReadOnly();
            CompositeId()
                .KeyProperty(x => x.SubjectId)
                .KeyProperty(x => x.GroupId);
            Map(x => x.Name);

        }
        
        
    }
}
