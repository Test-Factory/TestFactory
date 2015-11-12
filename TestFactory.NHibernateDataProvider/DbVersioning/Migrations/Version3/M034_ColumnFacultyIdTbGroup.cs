using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;

namespace TestFactory.NHibernateDataProvider.DbVersioning.Migrations.Version3
{
    [Migration(201511121825)]
    public class M034_ColumnFacultyIdTbGroup : Migration
    {
        public override void Up()
        {
            Execute.EmbeddedScript("ChangeFacultyToFacultyIdTbGroupAndChangeView.sql");
        }

        public override void Down()
        {
            Execute.EmbeddedScript("DeleteFacultyIdTbGroupAndOldView.sql");
        }
    }
}
