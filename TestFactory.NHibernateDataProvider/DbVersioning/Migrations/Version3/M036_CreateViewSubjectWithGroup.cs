using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;

namespace TestFactory.NHibernateDataProvider.DbVersioning.Migrations.Version3
{
    [Migration(201511131015)]
    public class M036_CreateViewSubjectWithGroup  : Migration
    {
        public override void Up()
        {
            Execute.EmbeddedScript("CreateViewSubjectWithGroup.sql");
        }

        public override void Down()
        {
            Execute.Sql("drop view SubjectWithGroup");
        }
    }
}
