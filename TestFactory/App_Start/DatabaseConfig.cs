﻿using TestFactory.NHibernateDataProvider.DbVersioning;

namespace TestFactory.App_Start
{
    public class DatabaseConfig
    {
        public static void MigrateDatabase(string connectionString)
        {
            var migrator = new Migrator(connectionString);
            migrator.MigrateDown(201511051002);
            migrator.MigrateToLatest();    
        }
    }
}