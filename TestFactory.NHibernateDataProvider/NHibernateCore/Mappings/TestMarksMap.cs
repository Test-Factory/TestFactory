﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using TestFactory.Business.Models;
using NHibernate;

namespace TestFactory.NHibernateDataProvider.NHibernateCore.Mappings
{
    public class TestMarksMap : ClassMap<Marks>
    {
        public TestMarksMap()
        {
            References(x => x.Students).Class<Students>();

            References(x => x.Category).Class<TestDescription>();

            Map(x => x.Mark);
        }
    }
}
