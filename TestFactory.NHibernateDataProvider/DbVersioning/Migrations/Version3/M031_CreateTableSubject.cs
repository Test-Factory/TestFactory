using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;

namespace TestFactory.NHibernateDataProvider.DbVersioning.Migrations.Version3
{
    [Migration(201511101037)]
    public class M031_CreateTableSubject: Migration 
    {
        public override void Up()
        {
            Execute.EmbeddedScript("CreateTableSubject.sql");
        }

        public override void Down()
        {
            Execute.Sql("drop table [Subject]");
        }
    }
}
