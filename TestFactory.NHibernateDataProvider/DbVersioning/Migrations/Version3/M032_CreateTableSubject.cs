using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;

namespace TestFactory.NHibernateDataProvider.DbVersioning.Migrations.Version3
{
    [Migration(201511120155)]
    public class M032_CreateTableSubject: Migration 
    {
        public override void Up()
        {
            Execute.EmbeddedScript("CreateTableSubject.sql");
        }

        public override void Down()
        {
            Execute.EmbeddedScript("DropSubject.sql");
        }
    }
}
