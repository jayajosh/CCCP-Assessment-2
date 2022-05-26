using AssignmentMain;
using DataGateway;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using ui_command;

namespace AssignmentTests
{
    [TestClass]
    public class EmployeeTests
    {
        
        EmployeeGateway eGateway = new EmployeeGateway();

        public EmployeeTests() {
            InitialisationGateway iGateway = new InitialisationGateway();
            iGateway.InitialiseOracleDatabase();
        }


        [TestMethod]
        public void TestNewEmployeeHasCorrectName()
        {
            
            Employee employee = new Employee("a");
            Assert.AreEqual("a", employee.EmpName);
        }
        
        [TestMethod]
        public void TestEmployeeGatewayAddsEmployee()
        {
            Employee employee = new Employee("a");
            int id = eGateway.AddEmployee(employee);
            Dictionary<int, Employee> employees = eGateway.GetAllEmployees();
            Assert.AreEqual(employee.EmpName, employees[id].EmpName);
        }

        [TestMethod]
        public void TestEmployeeGatewayFindsCorrectEmployee()
        {
            Employee employee = new Employee("a");
            int id = eGateway.AddEmployee(employee);
            Employee foundEmp = eGateway.FindEmployee(id);
            Assert.AreEqual(employee.EmpName, foundEmp.EmpName);
        }
    }
}
