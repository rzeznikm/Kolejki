using System;
using System.Collections.Generic;
using System.Data;
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

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private List<Person> m_oPersonList = null;
        private int id;

        public Window1()
        {
            InitializeComponent();
            InitBinding();

            
        }

        

        private void InitBinding()
        {
            m_oPersonList = new List<Person>();
            m_oPersonList.Add(new Person(1, "Jan", "Kowalski", 25));
            m_oPersonList.Add(new Person(2, "Adam", "Nowak", 24));
            m_oPersonList.Add(new Person(3, "Agnieszka", "Kowalczyk", 23));
            id = 4;
            lstPersons.ItemsSource = m_oPersonList;
        }

        private void BT_Add_Click(object sender, RoutedEventArgs e)
        {
            m_oPersonList.Add(new Person(id, TXB_1.Text, TXB_2.Text, int.Parse(TXB_3.Text)));
            lstPersons.ItemsSource = null;
            lstPersons.ItemsSource = m_oPersonList;
        }

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            // ... Create a List of objects.
            var items = new List<Person>();
            items.Add(new Person(1, "Jan", "Kowalski", 25));
            items.Add(new Person(2, "Adam", "Nowak", 24));
      

            // ... Assign ItemsSource of DataGrid.
            var grid = sender as DataGrid;
            grid.ItemsSource = items;
        }
    }
}
