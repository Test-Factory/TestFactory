using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;

namespace TestFactory.NHibernateDataProvider.DbVersioning.Migrations.Version2
{
     [Migration(201511051002)]
    public class M001_AverageMarksForFacultyView   : Migration
    {
         public override void Up()
         {
             Execute.EmbeddedScript("AverageMarkForFacultyView.sql");
         }

         public override void Down()
         {
             Execute.Sql(@"drop view AverageMarkForFaculty ");
         }
    }
}
