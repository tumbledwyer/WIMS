using System.Linq;
using NUnit.Framework;
using PeanutButter.TempDb.LocalDb;
using PeanutButter.TestUtils.Entity;
using PeanutButter.Utils.Entity;
using WhereIsMyShit.DbContexts;
using WhereIsMyShit.DbMigrations;
using WhereIsMyShit.Models;

namespace WhereIsMyShit.Tests
{
    [TestFixture]
    public class TestCatalogueDbContext: EntityTestFixtureBase
    {
        [Test]
        public void SaveChanges_GivenNewItem_ShouldPersistChanges()
        {
            //---------------Set up test pack-------------------

            using (var dbContext = GetContext())
            {
                var loanItem = new LoanItem { Name = "Strat" };
                dbContext.LoanItems.Add(loanItem);
                //---------------Assert Precondition----------------
                var itemBeforeSave = dbContext.LoanItems.SingleOrDefault(i => i.Name == "Strat");
                Assert.IsNull(itemBeforeSave);
                //---------------Execute Test ----------------------
                dbContext.SaveChanges();
                //---------------Test Result -----------------------
                var addedItem = dbContext.LoanItems.Single(i => i.Name == "Strat");
                Assert.AreEqual(loanItem, addedItem);
            }
        }
    }
}
