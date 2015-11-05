using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;

namespace TestFactory.NHibernateDataProvider.DbVersioning.Migrations.Version2
{
     [Migration(201511051002)]
    public class M004_AverageMarksForFacultyView   : Migration
    {
         public override void Up()
         {
             Execute.Sql(
                 @" 
                     create view AverageMarkForFaculty
                        as
                        select  g.Faculty,c.Code,  c.Id,round(AVG(Cast(value as float)),2) Average
                        from [Mark] m
                        join Student s on m.StudentId = s.Id
                        join [Group] g on s.GroupId = g.Id
                        join Category c on c.Id = m.CategoryId
                        group by g.Faculty, c.Code, c.Id
                ");
         }

         public override void Down()
         {
             Execute.Sql(
                 @"drop view AverageMarkForFaculty ");
         }
    }
}
