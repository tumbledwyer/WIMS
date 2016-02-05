using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using NSubstitute;
using NUnit.Framework;
using PeanutButter.TempDb.LocalDb;
using PeanutButter.Utils.Entity;
using WhereIsMyShit.App_Start;
using WhereIsMyShit.Models;

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
                var dbContext = new CatalogueDbContext(localDb.CreateConnection());
                var itemRepository = CreateItemRepository(dbContext);
                //---------------Assert Precondition----------------
                //---------------Execute Test ----------------------
                var loanItems = itemRepository.GetItems();
                //---------------Test Result -----------------------
                CollectionAssert.IsEmpty(loanItems);
            }
        }

        [Test]
        public void GetItems_GivenOneItemInCatalogue_ShouldReturnThatItem()
        {
            //---------------Set up test pack-------------------
            using (var localDb = new TempDBLocalDb())
            {
                //todo factory method
                var dbContext = new CatalogueDbContext(localDb.CreateConnection());
                var loanItem = new LoanItem { Name = "Chair"};
                //todo refactor the add functionanlity to a private method
                dbContext.Items.Add(loanItem);
                dbContext.SaveChangesWithErrorReporting();
                var itemRepository = CreateItemRepository(dbContext);
                //---------------Assert Precondition----------------
                //---------------Execute Test ----------------------
                var loanItems = itemRepository.GetItems();
                //---------------Test Result -----------------------
                CollectionAssert.AreEquivalent(new [] {loanItem}, loanItems);
            }
        }

        //[Test]
        //public void GetItems_GivenNoItemsInCatalogue_ShouldReturnEmptyList()
        //{
        //    //---------------Set up test pack-------------------
        //    var catalogue = Substitute.For<ICatalogueDbContext>();
        //    var dbItems = Substitute.For<IDbSet<ItemModel>>();
        //    var itemList = new List<ItemModel>();
        //    dbItems.AsQueryable<ItemModel>().GetEnumerator().Returns(itemList.GetEnumerator());
        //    catalogue.Items.Returns(dbItems);
        //    var itemRepository = CreateItemRepository(catalogue);
        //    //---------------Assert Precondition----------------
        //    //---------------Execute Test ----------------------
        //    var items = itemRepository.GetItems();
        //    //---------------Test Result -----------------------
        //    CollectionAssert.IsEmpty(items);
        //}

        //[Test]
        //public void GetItems_GivenOneItemInCatalogue_ShouldListWithThatItem()
        //{
        //    //---------------Set up test pack-------------------
        //    var item = new ItemModel();
        //    var catalogue = Substitute.For<ICatalogueDbContext>();
        //    var dbItems = Substitute.For<IDbSet<ItemModel>>();
        //    var itemList = new List<ItemModel> {item};
        //    dbItems.AsQueryable<ItemModel>().GetEnumerator().Returns(itemList.GetEnumerator());
        //    catalogue.Items.Returns(dbItems);
        //    var itemRepository = CreateItemRepository(catalogue);
        //    //---------------Assert Precondition----------------
        //    //---------------Execute Test ----------------------
        //    var items = itemRepository.GetItems();
        //    //---------------Test Result -----------------------
        //    CollectionAssert.Contains(items, item);
        //}

        //[Test]
        //public void Add_GivenItem_ShouldAddItemToCatalogue()
        //{
        //    //---------------Set up test pack-------------------
        //    var catalogue = Substitute.For<ICatalogueDbContext>();
        //    var dbItems = Substitute.For<IDbSet<ItemModel>>();
        //    catalogue.Items.Returns(dbItems);
        //    var itemRepository = CreateItemRepository(catalogue);
        //    var item = new ItemModel();
        //    //---------------Assert Precondition----------------
        //    //---------------Execute Test ----------------------
        //    itemRepository.Add(item);
        //    //---------------Test Result -----------------------
        //    dbItems.Received().Add(item);
        //    catalogue.Received().SaveChanges();
        //}

        //[Test]
        //public void Add_GivenNullItem_ShouldNotAddItemToCatalogue()
        //{
        //    //---------------Set up test pack-------------------
        //    var catalogue = Substitute.For<ICatalogueDbContext>();
        //    var dbItems = Substitute.For<IDbSet<ItemModel>>();
        //    catalogue.Items.Returns(dbItems);
        //    var itemRepository = CreateItemRepository(catalogue);
        //    //---------------Assert Precondition----------------
        //    //---------------Execute Test ----------------------
        //    itemRepository.Add(null);
        //    //---------------Test Result -----------------------
        //    dbItems.DidNotReceive().Add(null);
        //    catalogue.DidNotReceive().SaveChanges();
        //}

        //[Test]
        //public void FindByName_GivenExistingName_ShouldCallFindOnCatalogue()
        //{
        //    var catalogue = Substitute.For<ICatalogueDbContext>();
        //    var itemRepository = CreateItemRepository(catalogue);
        //    var dbItems = Substitute.For<IDbSet<ItemModel>>();
        //    const string itemName = "pot";
        //    var itemModel = new ItemModel {Name = "pot"};
        //    var itemModel2 = new ItemModel {Name = "kettle"};
        //    var itemList = new List<ItemModel> {itemModel, itemModel2 };
        //    catalogue.Items.Returns(dbItems);
        //    //---------------Assert Precondition----------------
        //    //---------------Execute Test ----------------------
        //    var findByName = itemRepository.FindByName(itemName);
        //    //---------------Test Result -----------------------
        //    Assert.AreEqual(itemModel2, findByName);
        //    //itemList.Received().Find(model => model != null);
        //}

        private static ItemRepository CreateItemRepository(ICatalogueDbContext catalogue)
        {
            return new ItemRepository(catalogue);
        }
    }
}
