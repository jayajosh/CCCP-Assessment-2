using AssignmentMain;
using DataGateway;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace AssignmentTests
{
    [TestClass]
    public class TransactionTests
    {
        TransactionGateway tGateway = new TransactionGateway();

        public TransactionTests()
        {
            InitialisationGateway iGateway = new InitialisationGateway();
            iGateway.InitialiseOracleDatabase();
        }

        private int GetEmployeeID() {
            EmployeeGateway eGateway = new EmployeeGateway();
            Employee employee = new Employee("a");
            int id = eGateway.AddEmployee(employee);
            return id;
        }


        [TestMethod]
        public void TestNewTransactionHasCorrectType()
        {
            TransactionLogEntry transaction = new TransactionLogEntry("Quantity Added", 1, "b", 2, 3, GetEmployeeID(), DateTime.Now);
            Assert.AreEqual("Quantity Added", transaction.TypeOfTransaction);
        }

        [TestMethod]
        public void TestNewTransactionHasCorrectID()
        {
            int id = GetEmployeeID();
            TransactionLogEntry transaction = new TransactionLogEntry("Quantity Added", 1, "b", 2, 3, GetEmployeeID(), DateTime.Now);
            Assert.AreEqual(1, transaction.ItemID);
        }

        [TestMethod]
        public void TestNewTransactionHasCorrectItemName()
        {
            TransactionLogEntry transaction = new TransactionLogEntry("Quantity Added", 1, "b", 2, 3, GetEmployeeID(), DateTime.Now);
            Assert.AreEqual("b", transaction.ItemName);
        }

        [TestMethod]
        public void TestNewTransactionHasCorrectPrice()
        {
            TransactionLogEntry transaction = new TransactionLogEntry("Quantity Added", 1, "b", 2, 3, GetEmployeeID(), DateTime.Now);
            Assert.AreEqual(2, transaction.ItemPrice);
        }

        [TestMethod]
        public void TestNewTransactionHasCorrectQuantity()
        {
            TransactionLogEntry transaction = new TransactionLogEntry("Quantity Added", 1, "b", 2, 3, GetEmployeeID(), DateTime.Now);
            Assert.AreEqual(3, transaction.Quantity);
        }

        [TestMethod]
        public void TestNewTransactionHasCorrectEmployeeName()
        {
            int eid = GetEmployeeID();
            TransactionLogEntry transaction = new TransactionLogEntry("Quantity Added", 1, "b", 2, 3, eid, DateTime.Now);
            Assert.AreEqual(eid, transaction.EmployeeID);
        }

        [TestMethod]
        public void TestNewTransactionHasCorrectCreationDate()
        {
            DateTime now = DateTime.Now;
            TransactionLogEntry transaction = new TransactionLogEntry("Quantity Added", 1, "b", 2, 3, GetEmployeeID(), now);
            Assert.AreEqual(now, transaction.DateAdded);
        }

        [TestMethod]
        public void TestInvalidValuesForNewTransactionProducesException()
        {
            TransactionLogEntry Transaction;
            Assert.ThrowsException<Exception>(
                () => Transaction = new TransactionLogEntry("", 0, "", 0, 0, GetEmployeeID(), DateTime.Now));
        }

        [TestMethod]
        public void TestInvalidValuesForNewTransactionProducesCorrectErrorMessage()
        {
            try
            {
                TransactionLogEntry Transaction = new TransactionLogEntry("", 0, "", 0, 0, 0, DateTime.Now);
            }
            catch (Exception e)
            {
                string expectedErrorMsg =
                    "ERROR: There is no transaction type; ID below 1; Item name is empty; Quantity below 1; ";
                Assert.AreEqual(expectedErrorMsg, e.Message);
            }
        }

        [TestMethod]
        public void TestTransactionGatewayAddsLog()
        {
            tGateway.AddTransaction("Quantity Added", 1, "b", 2, 3, GetEmployeeID());
            List<TransactionLogEntry> transactions = tGateway.GetTransactions();
            Assert.AreEqual(1, transactions[^1].ItemID);
        }

    }
}
