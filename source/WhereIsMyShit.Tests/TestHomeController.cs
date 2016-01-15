using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using WhereIsMyShit.Controllers;

namespace WhereIsMyShit.Tests
{
    [TestFixture]
    public class TestHomeController
    {
        [Test]
        public void Index_ShouldReturnAResult()
        {
            //---------------Set up test pack-------------------
            var controller = new HomeController();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var actionResult = controller.Index();
            //---------------Test Result -----------------------
            Assert.IsNotNull(actionResult);
        }
    }
}
