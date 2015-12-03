using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFactory.NHibernateDataProvider.DbVersioning.Migrations.Version3
{
    [Migration(201512030955)]
    public class M037_CreateSuperAdmin : Migration
    {
        public override void Up()
        {
            Execute.EmbeddedScript("CreateSuperAdmin.sql");
        }

        public override void Down()
        {
            Execute.EmbeddedScript("DropSuperAdmin.sql");
        }
    }
}
