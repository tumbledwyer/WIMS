using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using PeanutButter.TempDb.LocalDb;
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
            {
                var dbContext = new CatalogueDbContext(localDb.CreateConnection());
                var loanItem = new LoanItem { Name = "Strat" };
                dbContext.Items.Add(loanItem);
                //---------------Assert Precondition----------------
                var itemBeforeSave = dbContext.Items.SingleOrDefault(i => i.Name == "Strat");
                Assert.IsNull(itemBeforeSave);
                //---------------Execute Test ----------------------
                dbContext.SaveChanges();
                //---------------Test Result -----------------------
                var addedItem = dbContext.Items.Single(i => i.Name == "Strat");
                Assert.AreEqual(loanItem, addedItem);
            }
        }
    }
}
