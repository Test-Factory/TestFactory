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
    public class GroupsMap : ClassMap<Groups>
    {
        public GroupsMap()
        {
            Id(x => x.Id);

            Map(x => x.FullName);

            Map(x => x.ShortName);

            Map(x => x.Students);
        }
    }
}
