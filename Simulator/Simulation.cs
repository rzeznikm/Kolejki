using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QueryNetwork;

namespace Simulator
{
    class Simulation
    {
        public Network QNetwork;
        public Network RNetwork;
        public Network QNetworkClear;
        public Network RNetworkClear;
        public float beta;
        public float gamma;
        public int maxClient;
        public int maxInput;
        public List<int> clientsInput;
        public Random random;

        public Simulation(List<Node> nodes, int maxClient, int maxInput)
        {
            RNetwork = new Network(nodes, NetworkType.Random);
            QNetwork = new Network(nodes, NetworkType.QLearning);

            RNetworkClear = new Network(nodes, NetworkType.Random);
            QNetworkClear = new Network(nodes, NetworkType.QLearning);

            this.maxClient = maxClient;
            this.maxInput = maxInput;

            clientsInput = new List<int>();

            random = new Random();
            distributeClients();
        }

        public void StartQLearningSimulation(float beta, float gamma, NetworkType type)
        {
            this.beta = beta;
            this.gamma = gamma;
            int CycleCounter;
            

            if (type == NetworkType.QLearning)
            {
                System.IO.StreamWriter file = new System.IO.StreamWriter("SimpleQ.csv");
                string header = "beta;gamma;cycle;clients";
                file.WriteLine(header);

                CycleCounter = SimulateQNetworkForParam(beta, gamma);
                List<string> data = new List<string>();
                data.Add(beta.ToString());
                data.Add(gamma.ToString());
                data.Add(CycleCounter.ToString());
                data.Add(maxClient.ToString());
                file.WriteLine(string.Join(";", data.ToArray()) + ";" + QNetwork.getNodeTotalSummary());

                file.Close();
            }
            else
            {
                System.IO.StreamWriter file = new System.IO.StreamWriter("Random.csv");
                string header = "cycle;clients";
                file.WriteLine(header);
                for (int i = 0; i < 100; i++ )
                { 
                    CycleCounter = SimulateRNetworkForParam();
                List<string> data = new List<string>();
                data.Add(CycleCounter.ToString());
                data.Add(maxClient.ToString());
                file.WriteLine(string.Join(";", data.ToArray()) + ";" + RNetwork.getNodeTotalSummary());
                }
                file.Close();
            }

           
            

        }

        public void StartOtpimalization()
        {
            string header = "beta;gamma;cycle;clients";
            System.IO.StreamWriter file = new System.IO.StreamWriter("OptiQ.csv");
            file.WriteLine(header);
            Console.WriteLine("Start!");
            for (float beta = 0.1F; beta <= 1.0F; beta += 0.1F)
            {
                for (float gamma = 0.1F; gamma <= 1.0F; gamma += 0.1F)
                {
                    int CycleCounter = SimulateQNetworkForParam(beta, gamma);
                    List<string> data = new List<string>();
                    data.Add(beta.ToString("0.00"));
                    data.Add(gamma.ToString("0.00"));
                    data.Add(CycleCounter.ToString());
                    data.Add(maxClient.ToString());
                    file.WriteLine(string.Join(";", data.ToArray()) + ";" + QNetwork.getNodeTotalSummary());
                }
            }
            file.Close();
        }

        private int SimulateQNetworkForParam(float beta, float gamma)
        {
            QNetwork = QNetworkClear;
            QNetwork.alpha = beta;
            QNetwork.gamma = gamma;
            QNetwork.resetNodes();

            int CycleCounter = 0;
            if (maxClient > 0)
            {
                do
                {
                    if (CycleCounter < clientsInput.Count)
                    {
                        QNetwork.insertCustomer(clientsInput[CycleCounter]);
                    }
                    QNetwork.performTimeCirlce();
                    CycleCounter++;
                }
                while ((QNetwork.geTotalCustomerCount() < maxClient) || (!QNetwork.isEmpty()));
            }

            return CycleCounter;
        }

        private int SimulateRNetworkForParam()
        {
            RNetwork = RNetworkClear;
            RNetwork.resetNodes();

            int CycleCounter = 0;
            if (maxClient > 0)
            {
                do
                {
                    if (CycleCounter < clientsInput.Count)
                    {
                        RNetwork.insertCustomer(clientsInput[CycleCounter]);
                    }
                    RNetwork.performTimeCirlce();
                    CycleCounter++;
                }
                while ((RNetwork.geTotalCustomerCount() < maxClient) || (!RNetwork.isEmpty()));
            }

            return CycleCounter;
        }

        private void distributeClients()
        {
            int clientsToDistribute = maxClient;
            while (clientsToDistribute > 0)
            {

                int input = random.Next(0, maxInput);
                if (input >= clientsToDistribute)
                {
                    clientsInput.Add(clientsToDistribute);
                    clientsToDistribute = 0;
                }
                else
                {
                    clientsInput.Add(input);
                    clientsToDistribute -= input;
                }
            }
        }


        public static List<int> StringToIntList(string str)
        {
            string[] split = str.Split(',');
            List<int> ret = new List<int>();
            foreach (var s in split)
            {
                ret.Add(int.Parse(s));
            }
            return ret;
        }

        public static Node GetNodeFromLine (string line)
        {
            List<int> data = Simulation.StringToIntList(line);
            List<int> connections = new List<int>();
            for (int i = 4; i < data.Count; i++ )
            {
                connections.Add(data[i]);
            }

                return new Node(data[0], data[1], data[2], data[3], connections);
        }


    }
}
