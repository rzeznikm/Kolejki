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

        public Window1()
        {
            InitializeComponent();
            string selected_dept = (App.Current as App).DeptName;
        }

        private void BT_Run_Click(object sender, RoutedEventArgs e)
        {
            Random r = new Random();
            int n = r.Next(1, 100);
            if (n < (App.Current as App).percentage)
            {
                (App.Current as App).network.insertCustomer(1);
                clients++;
                TXB_Clients.Text = clients.ToString();
            }
            (App.Current as App).network.performTimeCirlce();
            var customerInNodes = (App.Current as App).network.getCustomerSumForEachNode();
                  
            for (int i = 1; i < customerInNodes.Count - 1; i++)
            {
                (App.Current as App).displaymodelList[i].customersNr = customerInNodes[i];
            }

            var clientsInNodes = (App.Current as App).network.getClientNumberForEachNode();

            for (int i = 1; i < clientsInNodes.Count - 1; i++)
            {
                (App.Current as App).displaymodelList[i].clientsNr = clientsInNodes[i];
            }

            runs++;
            TXB_Runs.Text = runs.ToString();

            Results.ItemsSource = null;
            Results.ItemsSource = (App.Current as App).displaymodelList;
        }
    }
}
