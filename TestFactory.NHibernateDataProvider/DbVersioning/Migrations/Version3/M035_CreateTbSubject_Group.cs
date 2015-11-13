using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;

namespace TestFactory.NHibernateDataProvider.DbVersioning.Migrations.Version3
{
    [Migration(201511130929)]
    public class M035_CreateTbSubject_Group : Migration
    {
        public override void Up()
        {
            Execute.EmbeddedScript("CreateTbSubject_Group.sql");
        }

        public override void Down()
        {
            Execute.EmbeddedScript("DropTbSubject_Group.sql");
        }
    }
}
