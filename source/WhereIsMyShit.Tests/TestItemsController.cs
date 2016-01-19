using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using NUnit.Framework;
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
            Assert.AreEqual(typeof(ViewResult) , result.GetType());
        }

        [Test]
        public void ViewResult_ShouldHaveItemModel()
        {
            //---------------Set up test pack-------------------
            var itemsController = CreateSut();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var actionResult = itemsController.Index();
            var viewResult = actionResult as ViewResult;
            //---------------Test Result -----------------------
            Assert.IsNotNull(viewResult.Model);
        }

        [Test]
        public void Add_GivenItem_ShouldAddItemToModels()
        {
            //---------------Set up test pack-------------------
            var itemsController = CreateSut();
            var itemModel = new ItemModel {Name = "something"};
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            itemsController.Add(itemModel);
            var viewResult = itemsController.Index() as ViewResult;
            var models = viewResult.Model as List<ItemModel>;
            //---------------Test Result -----------------------
            CollectionAssert.Contains(models, itemModel);

        }

        private static ItemsController CreateSut()
        {
            return new ItemsController();
        }
    }
}
