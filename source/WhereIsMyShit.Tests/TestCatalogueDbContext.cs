using System.Linq;
using NUnit.Framework;
using PeanutButter.TempDb.LocalDb;
using WhereIsMyShit.DbContexts;
using WhereIsMyShit.Models;

namespace WhereIsMyShit.Tests
{
    [TestFixture]
    public class TestCatalogueDbContext
    {
        [Test]
        public void SaveChanges_GivenNewItem_ShouldPersistChanges()
        {
            //---------------Set up test pack-------------------
            using (var localDb = new TempDBLocalDb())
            using (var dbContext = new CatalogueDbContext(localDb.CreateConnection()))
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
