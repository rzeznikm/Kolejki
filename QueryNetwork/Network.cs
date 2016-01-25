using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryNetwork
{
    public class Network
    {
        private List<Node> listOfNodes; // Lista systemów w sieci

        public Network(List<Node> list)
        {
            this.listOfNodes = list;
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
            for(int i = (listOfNodes.Count-1);i>0;i--)
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
                int nextNode = listOfNodes[node].getBestNextSystem();
                if(nextNode>0)
                {
                    listOfNodes[nextNode].insertCustomer();
                    updateRewards(ref listOfNodes, node, nextNode);
                }
            }
        }

        private void updateRewards(ref List<Node> nodes, int baseNode, int nextNode)
        {
            throw new NotImplementedException();
        }


    }
}

/*
 * Do sieci wchodzi nowe zgłoszenie
 * Zostaje przydzielone do systemu początkowego We (sztuczny lub wybrany z dostępnych).
 * Zwiększona zostaje aktualna liczba zgłoszeń w danym systemie: +1.
 *
 */

