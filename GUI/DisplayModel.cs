using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    public class DisplayModel
    {
        public int id { get; set; }
        public int serviceChannelsNumber { get; set; }
        public int queueMaxSize { get; set; }
        public int checkoutTime { get; set; }
        public string systems { get; set; }
        public int customersNr { get; set; }
        public int clientsNr { get; set; }
    }
}
