using Microsoft.VisualStudio.TestTools.UnitTesting;
using QueryNetwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryNetwork.Tests
{
    [TestClass()]
    public class NetworkTests
    {
        [TestMethod()]
        public void performTimeCirlce() //check first distribution of customers
        {
            List<int> customerInNodes = new List<int>();
            // one customer
            Network network = prepareTestNetwork();
            network.insertCustomer(1);
            network.performTimeCirlce();
            customerInNodes = network.getClientNumberForEachNode();
            Assert.AreEqual(1, customerInNodes[1]);
            // two customers
            network = prepareTestNetwork();
            network.insertCustomer(2);
            network.performTimeCirlce();
            customerInNodes = network.getClientNumberForEachNode();
            Assert.AreEqual(2, customerInNodes[1]);
            // three customers
            network = prepareTestNetwork();
            network.insertCustomer(3);
            network.performTimeCirlce();
            customerInNodes = network.getClientNumberForEachNode();
            Assert.AreEqual(2, customerInNodes[1]);
            Assert.AreEqual(1, customerInNodes[2]);
            // four customers
            network = prepareTestNetwork();
            network.insertCustomer(4);
            network.performTimeCirlce();
            customerInNodes = network.getClientNumberForEachNode();
            Assert.AreEqual(3, customerInNodes[1]);
            Assert.AreEqual(1, customerInNodes[2]);
            // max customers
            network = prepareTestNetwork();
            network.insertCustomer(11);
            network.performTimeCirlce();
            customerInNodes = network.getClientNumberForEachNode();
            Assert.AreEqual(6, customerInNodes[1]);
            Assert.AreEqual(5, customerInNodes[2]);
            // more than max customers - shoul be lost
            network = prepareTestNetwork();
            network.insertCustomer(14);
            network.performTimeCirlce();
            customerInNodes = network.getClientNumberForEachNode();
            Assert.AreEqual(6, customerInNodes[1]);
            Assert.AreEqual(5, customerInNodes[2]);


        }
        [TestMethod()]
        public void performTimeCirlceChckInTest() //check first distribution of customers
        {
            List<int> customerInNodes = new List<int>();
            // max customers
            Network network = prepareTestNetwork();
            network.insertCustomer(11);
            network.performTimeCirlce();
            network.performTimeCirlce();
            network.performTimeCirlce();
            network.performTimeCirlce();
            customerInNodes = network.getClientNumberForEachNode();
            Assert.AreEqual(4, customerInNodes[1]);
            Assert.AreEqual(5, customerInNodes[2]);
            network.performTimeCirlce();
            customerInNodes = network.getClientNumberForEachNode();
            Assert.AreEqual(4, customerInNodes[2]);
            network.performTimeCirlce();
            network.performTimeCirlce();
            customerInNodes = network.getClientNumberForEachNode();
            Assert.AreEqual(2, customerInNodes[1]);
            network.performTimeCirlce();
            network.performTimeCirlce();
            customerInNodes = network.getClientNumberForEachNode();
            Assert.AreEqual(3, customerInNodes[2]);
            network.performTimeCirlce();
            customerInNodes = network.getClientNumberForEachNode();
            Assert.AreEqual(0, customerInNodes[1]);
        }
        private Network prepareTestNetwork()
        {
            List<int> startSystemConections = new List<int>();
            startSystemConections.Add(1);
            startSystemConections.Add(2);
            List<int> connectionToLastSystem = new List<int>();
            connectionToLastSystem.Add(3);
            List<Node> nodes = new List<Node>();
            nodes.Add(new Node(0, -1, -1, 0, startSystemConections));
            nodes.Add(new Node(1, 2, 4, 3, connectionToLastSystem));
            nodes.Add(new Node(2, 1, 4, 4, connectionToLastSystem));
            nodes.Add(new Node(3, -1, -1, 0, new List<int>(0)));

            Network network = new Network(nodes, NetworkType.QLearning);
            return network;
        }
    }
}