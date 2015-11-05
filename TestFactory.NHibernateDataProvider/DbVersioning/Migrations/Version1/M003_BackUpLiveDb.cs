using FluentMigrator;

namespace TestFactory.NHibernateDataProvider.DbVersioning.Migrations
{
    [Migration(201510261139)]
    public class M003_BackUpLiveDb   : Migration
    {
        public override void Up()
        {
            Execute.EmbeddedScript("BackUpData.sql");
        }

        public override void Down()
        {
            Execute.EmbeddedScript("BackUpDataDown.sql");
        }
    }
}
