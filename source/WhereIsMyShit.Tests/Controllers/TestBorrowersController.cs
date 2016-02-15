using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using PeanutButter.RandomGenerators;
using WhereIsMyShit.Controllers;
using WhereIsMyShit.Models;
using WhereIsMyShit.Repositories;

namespace WhereIsMyShit.Tests.Controllers
{
    [TestFixture]
    public class TestBorrowersController : WimsDbContextPersistenceTestFixtureBase
    {
        //Todo implement repo
        [Ignore("Busy Implementing repository")]
        [Test]
        public void Create_GivenValidBorrowerDetails_ShouldAddBorrowerToWims()
        {
            //---------------Set up test pack-------------------
            using (var context = GetContext())
            {
                var repository = new BorrowerRepository(context);
                var controller = new BorrowersController(repository);
                var borrower = EntityBuilders.BorrowerBuilder.Create().WithRandomProps().Build();
                //---------------Assert Precondition----------------
                //---------------Execute Test ----------------------
                controller.Create(borrower);
                //---------------Test Result -----------------------
                CollectionAssert.Contains(repository.GetItems(), borrower);
            }
        }
    }
}
