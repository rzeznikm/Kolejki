﻿using System;
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
using System.Windows.Shapes;

namespace GUI
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private int runs = 0;
        private int clients = 0;
        private bool noMoreClients = false;

        public Window1()
        {
            InitializeComponent();
            string selected_dept = (App.Current as App).DeptName;
        }

        private void BT_Run_Click(object sender, RoutedEventArgs e)
        {
            Random r = new Random();
            int n = r.Next(1, 100);
            if (n < (App.Current as App).percentage && noMoreClients == false)
            {
                n = r.Next(1, 100);
                if(n < 60)
                {
                    (App.Current as App).network.insertCustomer(1);
                    (App.Current as App).randomNetwork.insertCustomer(1);
                    clients += 1;
                }
                if (n >= 60 && n < 90)
                {
                    (App.Current as App).network.insertCustomer(2);
                    (App.Current as App).randomNetwork.insertCustomer(2);
                    clients += 2;
                }
                if (n >= 90)
                {
                    (App.Current as App).network.insertCustomer(3);
                    (App.Current as App).randomNetwork.insertCustomer(3);
                    clients += 3;
                }
                TXB_Clients.Text = clients.ToString();
            }
            (App.Current as App).network.performTimeCirlce();
            (App.Current as App).randomNetwork.performTimeCirlce();

            var customerInNodes = (App.Current as App).network.getCustomerSumForEachNode();
            var customerInNodesRandom = (App.Current as App).randomNetwork.getCustomerSumForEachNode();
                  
            for (int i = 0; i < customerInNodes.Count; i++)
            {
                (App.Current as App).displaymodelList[i].customersNr = customerInNodes[i];
            }

            for (int i = 0; i < customerInNodesRandom.Count; i++)
            {
                (App.Current as App).displaymodelListRandom[i].customersNr = customerInNodesRandom[i];
            }

            var clientsInNodes = (App.Current as App).network.getClientNumberForEachNode();
            var clientsInNodesRandom = (App.Current as App).randomNetwork.getClientNumberForEachNode();

            for (int i = 0; i < clientsInNodes.Count; i++)
            {
                (App.Current as App).displaymodelList[i].clientsNr = clientsInNodes[i];
            }

            for (int i = 0; i < clientsInNodesRandom.Count; i++)
            {
                (App.Current as App).displaymodelListRandom[i].clientsNr = clientsInNodesRandom[i];
            }

            runs++;
            TXB_Runs.Text = runs.ToString();

            Results.ItemsSource = null;
            Results.ItemsSource = (App.Current as App).displaymodelList;

            ResultsRandom.ItemsSource = null;
            ResultsRandom.ItemsSource = (App.Current as App).displaymodelListRandom;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            noMoreClients = true;
        }
    }
}
