using System;
using FluentMigrator.Runner.Processors.SqlServer;
using PeanutButter.FluentMigrator;

namespace WhereIsMyShit.DbMigrations
{
    public class MigrationsRunner : DBMigrationsRunner<SqlServer2000ProcessorFactory>
    {
        public MigrationsRunner(string connectionString, Action<string> textWriterAction = null) : 
            base(typeof(MigrationsRunner).Assembly, connectionString, textWriterAction)
        {
        }
    }
}
