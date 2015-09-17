﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using TestFactory.Business.Models;
using NHibernate;

namespace NHibernateDataProviders.NHibernateCore.Mappings
{
    public class StudentMap : ClassMap<Student>
    {
        public StudentMap()
        {
            Id(x => x.Id);

            Map(x => x.FirstName);

            Map(x => x.LastName);

            References(x => x.Group).Class<Group>();
        }
    }
}