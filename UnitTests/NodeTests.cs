using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QueryNetwork;
using System.Collections.Generic;

namespace QueryNetwork.Tests
{
    [TestClass()]
    public class NodeTests
    {
        List<int> testList = new List<int>();
        Node testNode;
        [TestMethod()]
        public void insertCustomerTest()
        {
            testList.Add(0);
            testNode = new Node(1, 1, 2, 2, testList);
            Assert.AreEqual(0, testNode.customerCount);
            testNode.insertCustomer();
            Assert.AreEqual(1, testNode.customerCount);
            testNode.insertCustomer();
            testNode.insertCustomer();
            Assert.AreEqual(3, testNode.customerCount);
            testNode.insertCustomer();
            Assert.AreEqual(3, testNode.customerCount);
        }

        [TestMethod()]
        public void performTimeEventTest()
        {
            testList.Add(0);
            testNode = new Node(1, 1, 2, 2, testList);
            testNode.insertCustomer();
            testNode.insertCustomer();
            testNode.performTimeEvent();
            testNode.performTimeEvent();
            Assert.AreEqual(1, testNode.getNumberOfCheckouts());
        }
    }
}


