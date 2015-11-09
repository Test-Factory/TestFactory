

using FluentMigrator;

namespace TestFactory.NHibernateDataProvider.DbVersioning.Migrations.Version1
{
    [Migration(201510250916)]
    public class M012_DefaultData  : Migration
    {
        public override void Up()
        {
            Execute.EmbeddedScript("DefaultDataUp.sql");
        }

        public override void Down()
        {
            Execute.EmbeddedScript("DefaultDataDown.sql");
        }
    }
}
