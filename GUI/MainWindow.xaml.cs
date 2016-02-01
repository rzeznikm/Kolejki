using QueryNetwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Node> nodes = null;
        private int Id = 0;
        private Network network;
        private List<DisplayModel> displaymodelList;

        public MainWindow()
        {
            InitializeComponent();
            InitNetwork();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void BT_Add_Click(object sender, RoutedEventArgs e)
        {
            displaymodelList.Insert(Id, new DisplayModel()
            {
                id = Id,
                serviceChannelsNumber = int.Parse(TXB_1.Text),
                queueMaxSize = int.Parse(TXB_2.Text),
                checkoutTime = int.Parse(TXB_3.Text),
                systems = TXB_4.Text
            });
            Id++;
            displaymodelList[Id].id = Id;

            NodeList.ItemsSource = null;
            NodeList.ItemsSource = displaymodelList;
        }

        private void InitNetwork()
        {
            displaymodelList = new List<DisplayModel>();

            //----
            displaymodelList.Add(new DisplayModel() { id = 0, serviceChannelsNumber = -1, queueMaxSize = -1, checkoutTime = 0, systems = "1,2" });
            displaymodelList.Add(new DisplayModel() { id = 1, serviceChannelsNumber = -1, queueMaxSize = -1, checkoutTime = 0, systems = "0" });
            //----
            //---- TESTOWE 
            //displaymodelList.Add(new DisplayModel() { id = 0, serviceChannelsNumber = -1, queueMaxSize = -1, checkoutTime = 0, systems = "1,2" });
            //displaymodelList.Add(new DisplayModel() { id = 1, serviceChannelsNumber = 2, queueMaxSize = 4, checkoutTime = 3, systems = "3" });
            //displaymodelList.Add(new DisplayModel() { id = 2, serviceChannelsNumber = 1, queueMaxSize = 4, checkoutTime = 4, systems = "3" });
            //displaymodelList.Add(new DisplayModel() { id = 3, serviceChannelsNumber = -1, queueMaxSize = -1, checkoutTime = 0, systems = "0" });
            //-----

            Id = 1;
            NodeList.ItemsSource = displaymodelList;
        }

        private void BT_Run_Click(object sender, RoutedEventArgs e)
        {           
            Random r = new Random();
            int n = r.Next(1, 100);
            if(n > 50)
            {
                network.insertCustomer(1);               
            }
            network.performTimeCirlce();
            var customerInNodes = network.getClientNumberForEachNode();
            for(int i = 1; i < customerInNodes.Count-1; i++)
            {
                displaymodelList[i].customersNr = customerInNodes[i];
            }

            NodeList.ItemsSource = null;
            NodeList.ItemsSource = displaymodelList;
        }

        private static List<int> StringToIntList(string str)
        {
            string[] split = str.Split(',');
            List<int> ret = new List<int>();
            foreach (var s in split)
            {
                ret.Add(int.Parse(s));
            }
            return ret;
        }

        private void BT_Confirm_Click(object sender, RoutedEventArgs e)
        {
            nodes = new List<Node>();

            foreach (var model in displaymodelList)
            {
                Node node = new Node(model.id, model.serviceChannelsNumber, model.queueMaxSize, model.checkoutTime, StringToIntList(model.systems));
                nodes.Add(node);
            }
            network = new Network(nodes);
        }
    }
}
