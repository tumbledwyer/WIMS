using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeanutButter.TestUtils.Entity;
using PeanutButter.Utils.Entity;

namespace WhereIsMyShit.Tests
{
    public abstract class CatalogueDbContextPersistenceTestFixtureBase : EntityPersistenceTestFixtureBase<CatalogueDbContext>
    {
        public CatalogueDbContextPersistenceTestFixtureBase()
        {
            //Configure(false, connectionString => new CompositeDBMigrator(connectionString, true));
            
            //DisableDatabaseRegeneration();
            RunBeforeFirstGettingContext(Clear);
        }

        private void Clear(CatalogueDbContext ctx)
        {
            ctx.Items.Clear();
            ctx.SaveChangesWithErrorReporting();
        }
    }


    //public abstract class SomeContextPersistenceTestFixtureBase : EntityPersistenceTestFixtureBase<SomeContext>
    //{
    //    public SomeContextPersistenceTestFixtureBase()
    //    {
    //        Configure(false, connectionString => new CompositeDBMigrator(connectionString, true));
    //        DisableDatabaseRegeneration();
    //        RunBeforeFirstGettingContext(Clear);
    //    }

    //    private void Clear(SomeContext ctx)
    //    {
    //        ctx.SomeChildEntities.Clear();
    //        ctx.SomeEntities.Clear();
    //        ctx.SaveChangesWithErrorReporting();
    //    }
    //}


}
