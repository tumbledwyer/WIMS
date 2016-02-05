﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NSubstitute;
using NUnit.Framework;
using WhereIsMyShit.App_Start;
using WhereIsMyShit.Controllers;
using WhereIsMyShit.Models;

namespace WhereIsMyShit.Tests
{
    [TestFixture]
    public class TestItemsController
    {
        [Test]
        public void Index_ShouldReturnView()
        {
            //---------------Set up test pack-------------------
            var itemsController = CreateSut();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = itemsController.Index();
            //---------------Test Result -----------------------
            Assert.AreEqual(typeof(ViewResult), result.GetType());
        }

        [Test]
        public void Add_ShouldReturnView()
        {
            //---------------Set up test pack-------------------
            var itemsController = CreateSut();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = itemsController.Add();
            //---------------Test Result -----------------------
            Assert.AreEqual(typeof(ViewResult), result.GetType());
        }

        [Test]
        public void Index_Given2ItemsInCatalogue_ShouldReturn2ItemsInCatalogue()
        {
            //---------------Set up test pack-------------------
            var itemRepository = Substitute.For<IItemRepository>();
            var itemsController = CreateSut(itemRepository);
            var itemModel = CreateItem();
            var itemModel2 = CreateItem();
            var itemModels = new List<LoanItem> { itemModel, itemModel2};
            itemRepository.GetItems().Returns(itemModels);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var actionResult = itemsController.Index();
            //---------------Test Result -----------------------
            var viewResult = actionResult as ViewResult;
            Assert.AreEqual(itemModels, viewResult.Model);
        }

        [Test]
        public void Add_GivenValidItem_ShouldAddItemToCatalogue()
        {
            //---------------Set up test pack-------------------
            var itemRepository = CreateConcreteRepository();
            var itemsController = CreateSut(itemRepository);
            var item = CreateItem("something");
            var itemModels = itemRepository.GetItems();
            //---------------Assert Precondition----------------
            CollectionAssert.DoesNotContain(itemModels, item);
            //---------------Execute Test ----------------------
            itemsController.Add(item);
            //---------------Test Result -----------------------
            var models = itemRepository.GetItems();
            CollectionAssert.Contains(models, item);
        }

        [Test]
        public void Add_GivenValidationError_ShouldNotAddItemToCatalogue()
        {
            //---------------Set up test pack-------------------
            var itemRepository = CreateConcreteRepository();
            var itemsController = CreateSut(itemRepository);
            itemsController.ModelState.AddModelError("Name", "cake");
            var itemModel = CreateItem("");
            var itemModels = itemRepository.GetItems();
            //---------------Assert Precondition----------------
            CollectionAssert.DoesNotContain(itemModels, itemModel);
            //---------------Execute Test ----------------------
            itemsController.Add(itemModel);
            //---------------Test Result -----------------------
            var models = itemRepository.GetItems();
            CollectionAssert.DoesNotContain(models, itemModel);
        }

        [Test]
        public void Add_GivenItem_ShouldShowCatalogueOfItems()
        {
            //---------------Set up test pack-------------------
            var itemsController = CreateSut();
            var itemModel = CreateItem("something");
            //---------------Execute Test ----------------------
            var actionResult = itemsController.Add(itemModel) as RedirectToRouteResult;
            //---------------Test Result -----------------------
            Assert.AreEqual("Index", actionResult.RouteValues["action"]);
        }

        [Test]
        public void Add_GivenValidationError_ShouldStayOnSamePage()
        {
            //---------------Set up test pack-------------------
            var itemsController = CreateSut();
            itemsController.ModelState.AddModelError("Name", "cake");
            var itemModel = CreateItem("");
            //---------------Execute Test ----------------------
            var actionResult = itemsController.Add(itemModel) as ViewResult;
            //---------------Test Result -----------------------
            Assert.AreEqual("Add", actionResult.ViewName);
        }
        
        [Test]
        public void Add_GivenValidationError_ShouldReturnTheModel()
        {
            //---------------Set up test pack-------------------
            var itemsController = CreateSut();
            itemsController.ModelState.AddModelError("Name", "cake");
            var itemModel = CreateItem("");
            //---------------Execute Test ----------------------
            var actionResult = itemsController.Add(itemModel) as ViewResult;
            //---------------Test Result -----------------------
            Assert.AreEqual(itemModel, actionResult.Model);
        }

        [Test]
        public void Delete_GivenItem_ShouldRemoveItemFromCatalogue()
        {
            //---------------Set up test pack-------------------
            var repository = CreateConcreteRepository();
            var itemsController = CreateSut(repository);
            var item = CreateItem("Bob");
            repository.Add(item);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            itemsController.Delete(item.Name);
            //---------------Test Result -----------------------
            var items = repository.GetItems();
            CollectionAssert.DoesNotContain(items, item);
        }

        [Test]
        public void Delete_GivenItem_ShouldShowCatalogueOfRemainingItems()
        {
            //---------------Set up test pack-------------------
            var repository = CreateConcreteRepository();
            var itemsController = CreateSut(repository);
            var item = CreateItem("Dvd");
            repository.Add(item);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var actionResult = itemsController.Delete(item.Name) as RedirectToRouteResult;
            //---------------Test Result -----------------------
            Assert.AreEqual("Index", actionResult.RouteValues["action"]);
        }

        [Test]
        public void Delete_GivenItemThatDoesntExist_ShouldDeleteNothingAndShowCatalogue()
        {
            //---------------Set up test pack-------------------
            var repository = CreateConcreteRepository();
            var itemsController = CreateSut(repository);
            var item = CreateItem("Dvd");
            var item2 = CreateItem("Box");
            repository.Add(item);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var actionResult = itemsController.Delete(item2.Name) as RedirectToRouteResult;
            var itemModels = repository.GetItems();
            //---------------Test Result -----------------------
            CollectionAssert.AreEquivalent(new[] {item}, itemModels);
            Assert.AreEqual("Index", actionResult.RouteValues["action"]);
        }

        [Test]
        public void Delete_GivenItemThatDoesntExist_ShouldNotDeleteItem()
        {
            //---------------Set up test pack-------------------
            var repository = Substitute.For<IItemRepository>();
            var itemsController = CreateSut(repository);
            var item = CreateItem("Chair");
            repository.GetItems().Returns(new List<LoanItem> {item});
            var item2 = CreateItem();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            itemsController.Delete(item2.Name);
            //---------------Test Result -----------------------
            repository.DidNotReceive().Delete(item2.Name);
        }


        private static ItemsController CreateSut(IItemRepository itemRepository = null)
        {
            itemRepository = itemRepository ?? Substitute.For<IItemRepository>();
            return new ItemsController(itemRepository);
        }

        private static InMemoryItemRepository CreateConcreteRepository()
        {
            return new InMemoryItemRepository();
        }

        private static LoanItem CreateItem(string name = null)
        {
            return new LoanItem {Name = name};
        }
    }
}
