using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryNetwork
{

    public class Network
    {
        public float alpha { get; set; }
        public float gamma { get; set; }
        public NetworkType networkType { get; set; }

        private List<Node> listOfNodes; // Lista systemów w sieci

        public Network(List<Node> list, NetworkType type)
        {
            this.listOfNodes = list;
            this.networkType = type;
        }

        public void insertCustomer(int customersCount)
        {
            while (customersCount > 0)
            {
                listOfNodes[0].insertCustomer();
                customersCount--;
            }
        }
        public void performTimeCirlce()
        {
            //Wygenerować klientów insertCustomer(generuj ilośc zgłoszeń)?
            performTimeEventInAllNodes();
            moveCustomersinNetwork();
             
        }

        private void performTimeEventInAllNodes()
        {
            foreach (Node system in listOfNodes)
                system.performTimeEvent();
        }

        private void moveCustomersinNetwork()
        {
            for(int i = (listOfNodes.Count-1);i>=0;i--)
            {
                int freeCustomersCount = listOfNodes[i].getCheckoutsNumber();
                moveCustomersFromSystem(i, freeCustomersCount);
                listOfNodes[i].CheckInAllClients();
            }
        }

        private void moveCustomersFromSystem(int node, int customerCount)
        {
            for(int i=0;i<customerCount;i++)
            {
                int nextNode = listOfNodes[node].getNextSystem(networkType);
                if(nextNode > 0)
                {
                    listOfNodes[nextNode].insertCustomer();
                    updateRewards(ref listOfNodes, node, nextNode);
                }
            }
        }

        private void updateRewards(ref List<Node> nodes, int baseNode, int nextNode)
        {
            QLearningSolver.updateRewards(ref nodes, baseNode, nextNode, alpha, gamma);
        }

        public List<int> getClientNumberForEachNode()
        {
            List<int> clientNumber = new List<int>();
            foreach(var node in listOfNodes)
            {
                clientNumber.Add(node.customerCount);
            }
            return clientNumber;
        }

        public List<int> getCustomerSumForEachNode()
        {
            List<int> customerSum = new List<int>();
            foreach(Node node in listOfNodes)
            {
                customerSum.Add(node.customerSum);
            }
            return customerSum;
        }

        public bool isEmpty()
        {
            foreach(var node in listOfNodes)
            {
                if (node.customerCount > 0)
                {
                    return false;
                }
                else
                {
                    continue;
                }
            }
            return true;
        }

        public int geTotalCustomerCount()
        {
            return listOfNodes[0].customerSum;
        }

        public string getNodeTotalSummary()
        {
            List<string> data = new List<string>();
            foreach(var node in listOfNodes)
            {
                data.Add(node.customerSum.ToString());
            }
            return String.Join(";", data.ToArray());
        }

        public void resetNodes()
        {
            for (int i = 0; i < listOfNodes.Count; i++)
            {
                listOfNodes[i].resetNode();
            }
        }
    }
}



