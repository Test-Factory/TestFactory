using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;

namespace TestFactory.NHibernateDataProvider.DbVersioning.Migrations.Version3
{
    [Migration(201511261119)]
    public class M036_CreateTableSubjectMark : Migration
    {
        public override void Up()
        {
            Execute.EmbeddedScript("CreateTableSubjectMark.sql");
        }

        public override void Down()
        {
            Execute.EmbeddedScript("DropSubjectMark.sql");
        }
    }
}
