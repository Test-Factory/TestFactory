using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;

namespace TestFactory.NHibernateDataProvider.DbVersioning.Migrations.Version2
{
     [Migration(201511052216)]
    public class M022_StudentWithGroupView   : Migration
    {
         public override void Up()
         {
             Execute.EmbeddedScript("StudentWithGroupView.sql");
         }

         public override void Down()
         {
             Execute.Sql(@"drop view StudentWithGroup");
         }
    }
}
