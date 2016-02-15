using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using WhereIsMyShit.Repositories;
using WhereIsMyShit.Tests.Controllers;

namespace WhereIsMyShit.Tests.Repositories
{
    [TestFixture]
    public class TestBorrowerRepository : WimsDbContextPersistenceTestFixtureBase
    {
        [Test]
        public void Construct_GivenNullDbContext_ShouldThrowANE()
        {
            //---------------Set up test pack------------------n
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var exception = Assert.Throws<ArgumentNullException>(() => new BorrowerRepository(null));
            //---------------Test Result -----------------------
            Assert.AreEqual("dbContext", exception.ParamName);
        }

        [Test]
        public void GetItems_GivenNoItems_ShouldReturnEmptyList()
        {
            //---------------Set up test pack-------------------
            using (var context = GetContext())
            {
                var repository = new BorrowerRepository(context);
                //---------------Assert Precondition----------------
                //---------------Execute Test ----------------------
                var borrowers = repository.GetItems();
                //---------------Test Result -----------------------
                CollectionAssert.IsEmpty(borrowers);
            }
        }


        [Test]
        public void GetItems_GivenOneItem_ShouldReturnThatItem()
        {
            //---------------Set up test pack-------------------
            using (var context = GetContext())
            {
                var repository = new BorrowerRepository(context);

                var borrower = EntityBuilders.BorrowerBuilder.Create().WithRandomProps().Build();

                context.Borrowers.Add(borrower);
                context.SaveChanges();
                //---------------Assert Precondition----------------
                //---------------Execute Test ----------------------
                var borrowers = repository.GetItems();
                //---------------Test Result -----------------------
                CollectionAssert.AreEquivalent(new[] {borrower}, borrowers);
            }
        }

    }
}
