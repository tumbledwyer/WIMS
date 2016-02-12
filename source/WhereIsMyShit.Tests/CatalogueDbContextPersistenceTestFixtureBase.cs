﻿using PeanutButter.TestUtils.Entity;
using PeanutButter.Utils.Entity;
using WhereIsMyShit.DbContexts;
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