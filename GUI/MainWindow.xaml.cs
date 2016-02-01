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
        private int percentLocal = 0;

        public MainWindow()
        {
            InitializeComponent();
            InitNetwork();

            (App.Current as App).DeptName = "test";
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void BT_Add_Click(object sender, RoutedEventArgs e)
        {
            (App.Current as App).displaymodelList.Insert(Id, new DisplayModel()
            {
                id = Id,
                serviceChannelsNumber = int.Parse(TXB_1.Text),
                queueMaxSize = int.Parse(TXB_2.Text),
                checkoutTime = int.Parse(TXB_3.Text),
                systems = TXB_4.Text
            });
            Id++;
            (App.Current as App).displaymodelList[Id].id = Id;

            NodeList.ItemsSource = null;
            NodeList.ItemsSource = (App.Current as App).displaymodelList;
        }

        private void InitNetwork()
        {
            (App.Current as App).displaymodelList = new List<DisplayModel>();

            //----
            (App.Current as App).displaymodelList.Add(new DisplayModel() { id = 0, serviceChannelsNumber = -1, queueMaxSize = -1, checkoutTime = 0, systems = "1,2" });
            (App.Current as App).displaymodelList.Add(new DisplayModel() { id = 1, serviceChannelsNumber = -1, queueMaxSize = -1, checkoutTime = 0, systems = "0" });
            //----
            //---- TESTOWE 
            //(App.Current as App).displaymodelList.Add(new DisplayModel() { id = 0, serviceChannelsNumber = -1, queueMaxSize = -1, checkoutTime = 0, systems = "1,2" });
            //(App.Current as App).displaymodelList.Add(new DisplayModel() { id = 1, serviceChannelsNumber = 2, queueMaxSize = 4, checkoutTime = 3, systems = "3" });
            //(App.Current as App).displaymodelList.Add(new DisplayModel() { id = 2, serviceChannelsNumber = 1, queueMaxSize = 4, checkoutTime = 4, systems = "3" });
            //(App.Current as App).displaymodelList.Add(new DisplayModel() { id = 3, serviceChannelsNumber = -1, queueMaxSize = -1, checkoutTime = 0, systems = "0" });
            //-----

            Id = 1;
            NodeList.ItemsSource = (App.Current as App).displaymodelList;
        }

        private void BT_Run_Click(object sender, RoutedEventArgs e)
        {
            (App.Current as App).percentage = percentLocal;
            Window1 win1 = new Window1();
            win1.Show();         
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

            foreach (var model in (App.Current as App).displaymodelList)
            {
                Node node = new Node(model.id, model.serviceChannelsNumber, model.queueMaxSize, model.checkoutTime, StringToIntList(model.systems));
                nodes.Add(node);
            }
            (App.Current as App).network = new Network(nodes);
        }

        private void NodeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            percentLocal = (int) Slider1.Value;
            TXB_5.Text = percentLocal.ToString();
        }
    }
}
