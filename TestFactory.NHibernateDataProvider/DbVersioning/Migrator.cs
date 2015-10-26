using System.Data.SqlClient;
using System.Diagnostics;
using System.Reflection;
using FluentMigrator;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Announcers;
using FluentMigrator.Runner.Initialization;

namespace TestFactory.NHibernateDataProvider.DbVersioning
{
    public class Migrator
    {
        private readonly string connectionString;

        public Migrator(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public class MigrationOptions: IMigrationProcessorOptions
        {
            public bool PreviewOnly { get; set; }
            public string ProviderSwitches { get; set; }
            public int Timeout { get; set; }
        }

        public void MigrateToLatest()
        {
            //CreateDatabaseIfNotExist();

            var announcer = new TextWriterAnnouncer(s => Debug.WriteLine(s));
            var assembly = Assembly.GetExecutingAssembly();

            var migrationContext = new RunnerContext(announcer);
            var options = new MigrationOptions { PreviewOnly = false, Timeout = 60 };
            var factory = new FluentMigrator.Runner.Processors.SqlServer.SqlServer2012ProcessorFactory();
            using (var processor = factory.Create(connectionString, announcer, options))
            {
                var runner = new MigrationRunner(assembly, migrationContext, processor);
                runner.MigrateUp(true);
            }
        }

        public void CreateDatabaseIfNotExist()
        {
            var builder = new SqlConnectionStringBuilder(this.connectionString);
            var dbName = builder.InitialCatalog;
            
            builder.InitialCatalog = string.Empty;
            using (var connection = new SqlConnection(builder.ConnectionString))
            {
                 var command = new SqlCommand(string.Format(@"
                   IF  NOT EXISTS (SELECT name FROM sys.databases WHERE name = '{0}')
                    CREATE DATABASE {0}", dbName), connection);// "IF..." don`t work on staging
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
