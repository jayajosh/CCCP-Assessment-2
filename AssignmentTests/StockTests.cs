using AssignmentMain;
using DataGateway;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace AssignmentTests
{
    [TestClass]
    public class StockTests
    {

        public StockTests()
        {
            InitialisationGateway iGateway = new InitialisationGateway();
            iGateway.InitialiseOracleDatabase();
        }

        StockGateway sGateway = new StockGateway();

        [TestMethod]
        public void TestItemGetsAdded()
        {
            ItemDTO item = sGateway.AddItem(97, "a", 1);
            List<ItemDTO> items = sGateway.GetItems();
            Assert.AreEqual(97, items[^1].ID);
        }

        [TestMethod]
        public void TestStockGatewayFindsItem()
        {
            ItemDTO item = sGateway.AddItem(98, "a", 1);
            Assert.AreEqual(98, sGateway.FindItem(98).ID);
        }

        [TestMethod]
        public void TestStockGatewayGetsItems()
        {
            ItemDTO item = sGateway.AddItem(99, "a", 1);
            Assert.IsNotNull(sGateway.GetItems());
        }
    }
}
