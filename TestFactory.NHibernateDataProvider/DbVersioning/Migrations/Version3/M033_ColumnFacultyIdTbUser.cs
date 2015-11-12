﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;

namespace TestFactory.NHibernateDataProvider.DbVersioning.Migrations.Version3
{
    [Migration(201511121234)]
    public class M033_ColumnFacultyIdTbUser     : Migration
    {
        public override void Up()
        {
            Execute.EmbeddedScript("ChangeFacultyToFacultyIdTbUser.sql");
        }

        public override void Down()
        {
            Execute.EmbeddedScript("DeleteFucultyIdTbUser.sql");
        }
    }
}
