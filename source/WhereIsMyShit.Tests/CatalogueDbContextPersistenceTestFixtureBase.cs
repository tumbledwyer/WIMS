using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeanutButter.FluentMigrator;
using PeanutButter.TestUtils.Entity;
using PeanutButter.Utils.Entity;
using WhereIsMyShit.DbMigrations;

namespace WhereIsMyShit.Tests
{
    public abstract class CatalogueDbContextPersistenceTestFixtureBase : EntityPersistenceTestFixtureBase<CatalogueDbContext>
    {
        public CatalogueDbContextPersistenceTestFixtureBase()
        {
            Configure(false, connectionString => new MigrationsRunner(connectionString));
            DisableDatabaseRegeneration();
            RunBeforeFirstGettingContext(Clear);
        }

        private void Clear(CatalogueDbContext ctx)
        {
            ctx.Items.Clear();
            ctx.SaveChangesWithErrorReporting();
        }
    }
}
