using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QueryNetwork;

namespace Simulator
{
    class Program
    {
        static void Main(string[] args)
        {
            string line;
            List<int> nodeDescription = new List<int>();
            List<Node> nodes = new List<Node>();
            System.IO.StreamReader file = new System.IO.StreamReader("c:\\test2.txt");
            while ((line = file.ReadLine()) != null)
            {
                nodes.Add(Simulation.GetNodeFromLine(line));
            }

            Simulation simulation = new Simulation(nodes, 1000, 5);
            
            //simulation.StartQLearningSimulation(0.5F, 0.5F, NetworkType.QLearning);
            simulation.StartQLearningSimulation(0.5F, 0.5F, NetworkType.Random);
            
            simulation.StartOtpimalization();

            file.Close();

            Console.WriteLine("DONE!");

            // Suspend the screen.
            Console.ReadLine();

        }
    }
}
