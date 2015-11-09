using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;

namespace TestFactory.NHibernateDataProvider.DbVersioning.Migrations.Version2
{
    [Migration(201511091103)]
    public class M023_FrequencyMarkForFacultyByCategoryView : Migration 
    {
        public override void Up()
        {
            Execute.EmbeddedScript("FrequencyMarkForFacultyByCategoryView.sql");
        }

        public override void Down()
        {
            Execute.Sql(@"drop view FrequencyMarkForFacultyByCategory");
        }
    }
}
