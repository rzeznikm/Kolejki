using QueryNetwork;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace GUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public string DeptName { get; set; }

        public Network network { get; set; }

        public int percentage { get; set; }

        public List<DisplayModel> displaymodelList { get; set; }
    }
}
