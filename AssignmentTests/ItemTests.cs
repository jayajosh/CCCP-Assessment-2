using AssignmentMain;
using DataGateway;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AssignmentTests
{
    [TestClass]
    public class ItemTests
    {
        public ItemTests()
        {
            InitialisationGateway iGateway = new InitialisationGateway();
            iGateway.InitialiseOracleDatabase();
        }

        [TestMethod]
        public void TestNewItemHasCorrectId()
        {
            Item item = new Item(1, "a", 1, DateTime.Now);
            Assert.AreEqual(1, item.ID);
        }

        [TestMethod]
        public void TestNewItemHasCorrectName()
        {
            Item item = new Item(1, "a", 1, DateTime.Now);
            Assert.AreEqual("a", item.Name);
        }

        [TestMethod]
        public void TestNewItemHasCorrectQuantity()
        {
            Item item = new Item(2, "a", 1, DateTime.Now);
            Assert.AreEqual(1, item.Quantity);
        }

        [TestMethod]
        public void TestNewItemHasCorrectCreationDate()
        {
            DateTime now = DateTime.Now;
            Item item = new Item(2, "a", 1, now);
            Assert.AreEqual(now, item.DateCreated);
        }

        [TestMethod]
        public void TestInvalidValuesForNewItemProducesException()
        {
            Item item;
            Assert.ThrowsException<Exception>(
                () => item = new Item(0, "", 0, DateTime.Now));
        }

        [TestMethod]
        public void TestInvalidValuesForNewItemProducesCorrectErrorMessage()
        {
            try
            {
                Item item = new Item(0, "", 0, DateTime.Now);
            }
            catch (Exception e)
            {
                string expectedErrorMsg =
                    "ERROR: ID below 1; Quantity below 1; Item name is empty; ";
                Assert.AreEqual(expectedErrorMsg, e.Message);
            }
        }
    }
}
