using FluentMigrator;

namespace TestFactory.NHibernateDataProvider.DbVersioning.Migrations
{
    [Migration(201510220252)]
    public class M001_Initial: Migration 
    {
        public override void Up()
        {
            Execute.EmbeddedScript("CreateTablesUp.sql");
        }

        public override void Down()
        {
            Execute.EmbeddedScript("CreateTablesDown.sql");
        }
    }
}
