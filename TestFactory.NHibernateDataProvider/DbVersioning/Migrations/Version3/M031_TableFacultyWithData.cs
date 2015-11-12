using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;

namespace TestFactory.NHibernateDataProvider.DbVersioning.Migrations.Version3
{
    [Migration(201511101037)]
    public class M031_TableFacultyWithData   : Migration
    {
        public override void Up()
        {
            Execute.EmbeddedScript("CreateTableFacultyWithDefaultData.sql");
        }


        public override void Down()
        {
            Execute.Sql("drop table [Faculty]");
        }
    }
}
