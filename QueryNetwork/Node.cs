using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryNetwork
{
    class Node
    {
        private int id { get;} // Numer systemu 
        private int serviceChannelsNumber { get;} // ilośc kanałów obsługi (-1 dla nieskończoności)
        private int queueMaxSize { get; } // Rozmiar kolejki (-1 dla nieskończoności)
        private int customerCount { get; set; } // Aktualna liczba zgłoszeń
        private int checkoutTime { get; } // Czas obsługi dla danej klasy zgłoszenia
        private List<int> timeCounter { get; } //Licznik czasu obsługi
        private Dictionary<int, float> nextSystemsValues { get; } //Następne systemy i wartości Oczekiwane

        public Node(int id, int serviceChannelsNumber, int queueMaxSize, int checkoutTime, List<int> systems)
        {
            this.id = id;
            this.serviceChannelsNumber = serviceChannelsNumber;
            this.queueMaxSize = queueMaxSize;
            this.customerCount = 0;
            this.checkoutTime = checkoutTime;
            this.timeCounter = new List<int>();
            this.nextSystemsValues = prepareNextSystemDictionaty(systems);
        }

        private Dictionary<int, float> prepareNextSystemDictionaty(List<int> systems)
        {
            Dictionary<int, float> systemValue = new Dictionary<int, float>();
            foreach (int system in systems)
            {
                systemValue.Add(system, 0);
            }
            return systemValue;
        }

        public void insertCustomer()
        {
            if(infChanel() || infQueue() || avaliablePlaceExists())
            {
                customerCount++;
                timeCounter.Add(checkoutTime);
            }
        }

        private bool infChanel()
        {
            if (serviceChannelsNumber == -1)
                return true;
            return false;
        }

        private bool infQueue()
        {
            if (queueMaxSize == -1)
                return true;
            return false;
        }

        private bool avaliablePlaceExists()
        {
            if (customerCount < (queueMaxSize + serviceChannelsNumber))
                return true;
            else
                return false;
        }

        public void performTimeEvent()
        {
            if (infChanel())
            {
                decrementTimerForEachChanel(timeCounter.Count);
            }
            else
            {
                decrementTimerForEachChanel(serviceChannelsNumber);
            }
        }

        private void decrementTimerForEachChanel(int chanelNumber)
        {
            for (int i = 0; i < chanelNumber; i++)
                if (timeCounter[i] > 0)
                    timeCounter[i]--;
        }

        public int getNumberOfCheckouts()
        {
            int checkoutsNumber =0;
            foreach (int timer in timeCounter)
                if (timer == 0)
                    checkoutsNumber++;
            return checkoutsNumber;
        }

        public float getMaximumReward()
        {
            return nextSystemsValues.Values.Max();
        }

        public int getBestNextSystem()
        {
            KeyValuePair<int, float> max = new KeyValuePair<int, float>();
            foreach(var sys in nextSystemsValues)
            {
                if (sys.Value > max.Value)
                    max = sys;
            }
            return max.Key;
        }

        public int getCheckoutsNumber()
        {
            int numb = 0;
            foreach (var time in timeCounter)
                if (time == 0)
                    numb++;
            return numb;
        }

        public void CheckInAllClients()
        {
            for(int i=0; i<timeCounter.Count;i++)
            {
                if (timeCounter[i] == 0)
                {
                    timeCounter.RemoveAt(i);
                    customerCount--;
                }
            }
        }

    }
}
