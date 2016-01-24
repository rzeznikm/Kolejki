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
        private int systemMaxSize; // Rozmiar systemu (-1 dla nieskończoności)
        private int queueMaxSize; // Rozmiar kolejki (-1 dla nieskończoności)
        private int itemCount; // Aktualna liczba zgłoszeń
        private KeyValuePair<int, double> operationTime; // Czas obsługi dla danej klasy zgłoszenia
        private List<Link> listOfLinks; // Lista połączeń wychodzących z systemu

    }
}
