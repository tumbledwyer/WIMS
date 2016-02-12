using System;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using PeanutButter.TempDb.LocalDb;
using WhereIsMyShit.DbContexts;
using WhereIsMyShit.Models;
using WhereIsMyShit.Repositories;

namespace WhereIsMyShit.Tests
{
    [TestFixture]
    public class TestItemRepository 
    {
        [Test]
        public void Construct_GivenNullDbContext_ShouldThrowException()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var exception = Assert.Throws<ArgumentNullException>(() => new ItemRepository(null));
            //---------------Test Result -----------------------
            Assert.AreEqual("catalogue", exception.ParamName);
        }

        [Test]
        public void Construct_GivenDbContext_ShouldNotThrowException()
        {
            //---------------Set up test pack-------------------
            var catalogue = Substitute.For<ICatalogueDbContext>();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            Assert.DoesNotThrow(() => new ItemRepository(catalogue));
            //---------------Test Result -----------------------
        }

        [Test]
        public void GetItems_GivenNoItemsInCatalogue_ShouldReturnEmptyList()
        {
            //---------------Set up test pack-------------------
            using (var localDb = new TempDBLocalDb())
            {
                using (var dbContext = CreateDbContext(localDb))
                {
                    var itemRepository = CreateItemRepository(dbContext);
                    //---------------Assert Precondition----------------
                    //---------------Execute Test ----------------------
                    var loanItems = itemRepository.GetItems();
                    //---------------Test Result -----------------------
                    CollectionAssert.IsEmpty(loanItems);
                }
            }
        }

        [Test]
        public void GetItems_GivenOneItemInCatalogue_ShouldReturnThatItem()
        {
            //---------------Set up test pack-------------------
            using (var localDb = new TempDBLocalDb())
            {
                using (var dbContext = CreateDbContext(localDb))
                {
                    var loanItem = new LoanItem {Name = "Chair"};
                    AddItem(dbContext, loanItem);
                    var itemRepository = CreateItemRepository(dbContext);
                    //---------------Assert Precondition----------------
                    //---------------Execute Test ----------------------
                    var loanItems = itemRepository.GetItems();
                    //---------------Test Result -----------------------
                    CollectionAssert.AreEquivalent(new[] { loanItem }, loanItems);
                }
            }
        }

        [Test]
        public void FindByName_GivenNoMatchingItemName_ShouldReturnNull()
        {
            //---------------Set up test pack-------------------
            using (var localDb = new TempDBLocalDb())
            {
                using (var dbContext = CreateDbContext(localDb))
                {
                    var loanItem = new LoanItem { Name = "Lamp"};
                    AddItem(dbContext, loanItem);
                    var itemRepository = CreateItemRepository(dbContext);
                    //---------------Assert Precondition----------------
                    //---------------Execute Test ----------------------
                    var foundItem = itemRepository.FindByName("Fan");
                    //---------------Test Result -----------------------
                    Assert.IsNull(foundItem);
                }
            }
        }

        [Test]
        public void FindByName_GivenMatchingItemName_ShouldReturnLoanItem()
        {
            //---------------Set up test pack-------------------
            using (var localDb = new TempDBLocalDb())
            {
                using (var dbContext = CreateDbContext(localDb))
                {
                    var loanItem = new LoanItem {Name = "Lamp"};
                    AddItem(dbContext, loanItem);
                    var itemRepository = CreateItemRepository(dbContext);
                    //---------------Assert Precondition----------------
                    //---------------Execute Test ----------------------
                    var foundItem = itemRepository.FindByName("Lamp");
                    //---------------Test Result -----------------------
                    Assert.AreEqual(loanItem, foundItem);
                }
            }
        }

        [Test]
        public void Add_GivenLoanItem_ShouldAddItCatalogue()
        {
            //---------------Set up test pack-------------------
            using (var localDb = new TempDBLocalDb())
            {
                using (var dbContext = CreateDbContext(localDb))
                {
                    var itemRepository = CreateItemRepository(dbContext);
                    var loanItem = new LoanItem {Name = "Strat"};
                    //---------------Assert Precondition----------------
                    //---------------Execute Test ----------------------
                    itemRepository.Add(loanItem);
                    //---------------Test Result -----------------------
                    var addedItem = dbContext.Items.Single(i => i.Name == "Strat");
                    Assert.AreEqual(loanItem, addedItem);
                }
            }
        }

        [Test]
        public void FindById_GivenMatchingItemId_ShouldReturnLoanItem()
        {
            //---------------Set up test pack-------------------
            using (var localDb = new TempDBLocalDb())
            {
                using (var dbContext = CreateDbContext(localDb))
                {
                    var loanItem = new LoanItem {Name = "Lamp"};
                    AddItem(dbContext, loanItem);
                    var id = dbContext.Items.Single(i => i.Name == "Lamp").Id;
                    var itemRepository = CreateItemRepository(dbContext);
                    //---------------Assert Precondition----------------
                    //---------------Execute Test ----------------------
                    var foundItem = itemRepository.FindById(id);
                    //---------------Test Result -----------------------
                    Assert.AreEqual(loanItem, foundItem);
                }
            }
        }

        [Test]
        public void FindById_GivenMatchingNoItemId_ShouldReturnNull()
        {
            //---------------Set up test pack-------------------
            using (var localDb = new TempDBLocalDb())
            {
                using (var dbContext = CreateDbContext(localDb))
                {
                    var loanItem = new LoanItem {  Name = "Lamp" };
                    AddItem(dbContext, loanItem);
                    var itemRepository = CreateItemRepository(dbContext);
                    //---------------Assert Precondition----------------
                    //---------------Execute Test ----------------------
                    var foundItem = itemRepository.FindById(777);
                    //---------------Test Result -----------------------
                    Assert.IsNull(foundItem);
                }
            }
        }

        [Test]
        public void Delete_GivenMatchingNoItemId_ShouldNotDeleteAnyItems()
        {
            //---------------Set up test pack-------------------
            using (var localDb = new TempDBLocalDb())
            {
                using (var dbContext = CreateDbContext(localDb))
                {
                    var loanItem = new LoanItem {Name = "Lamp"};
                    AddItem(dbContext, loanItem);
                    var itemRepository = CreateItemRepository(dbContext);
                    //---------------Assert Precondition----------------
                    //---------------Execute Test ----------------------
                    itemRepository.Delete(777);
                    //---------------Test Result -----------------------
                    var loanItems = dbContext.Items;
                    CollectionAssert.AreEquivalent(new[] { loanItem }, loanItems);
                }
            }
        }

        [Test]
        public void Delete_GivenMatchingItemId_ShouldNotDeleteLoanItems()
        {
            //---------------Set up test pack-------------------
            using (var localDb = new TempDBLocalDb())
            {
                using (var dbContext = CreateDbContext(localDb))
                {
                    var loanItem = new LoanItem { Name = "Lamp" };
                    AddItem(dbContext, loanItem);
                    var id = dbContext.Items.Single(i => i.Name == "Lamp").Id;
                    var itemRepository = CreateItemRepository(dbContext);
                    //---------------Assert Precondition----------------
                    //---------------Execute Test ----------------------
                    itemRepository.Delete(id);
                    //---------------Test Result -----------------------
                    var loanItems = dbContext.Items;
                    CollectionAssert.IsEmpty(loanItems);
                }
            }
        }
        
        private static void AddItem(CatalogueDbContext dbContext, LoanItem loanItem)
        {
            dbContext.Items.Add(loanItem);
            dbContext.SaveChanges();
        }

        private static CatalogueDbContext CreateDbContext(TempDBLocalDb localDb)
        {
            return new CatalogueDbContext(localDb.CreateConnection());
        }
        
        private static ItemRepository CreateItemRepository(ICatalogueDbContext catalogue)
        {
            return new ItemRepository(catalogue);
        }
    }
}
