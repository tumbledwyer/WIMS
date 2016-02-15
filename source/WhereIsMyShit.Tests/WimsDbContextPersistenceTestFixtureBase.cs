using PeanutButter.TestUtils.Entity;
using PeanutButter.Utils.Entity;
using WhereIsMyShit.DbContexts;
using WhereIsMyShit.DbMigrations;

namespace WhereIsMyShit.Tests
{
    public abstract class WimsDbContextPersistenceTestFixtureBase : EntityPersistenceTestFixtureBase<WimsDbContext>
    {
        public WimsDbContextPersistenceTestFixtureBase()
        {
            Configure(false, connectionString => new MigrationsRunner(connectionString));
            DisableDatabaseRegeneration();
            RunBeforeFirstGettingContext(Clear);
        }

        public void Clear(WimsDbContext ctx)
        {
            ctx.LoanItems.Clear();
            ctx.SaveChangesWithErrorReporting();
        }
    }
}
