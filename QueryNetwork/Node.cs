using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryNetwork
{
    class Node
    {
        private int id; // Numer systemu
        private int serviceChannels; // ilośc kanałów obsługi (-1 dla nieskończoności)
        private int queueMaxSize; // Rozmiar kolejki (-1 dla nieskończoności)
        private int itemCount; // Aktualna liczba zgłoszeń
        private int operationTime; // Czas obsługi dla danej klasy zgłoszenia
        private List<int> timeCounter; //Licznik czasu obsługi
        private Dictionary<Node, float> nextSystems; //Następne systemy


    }
}
