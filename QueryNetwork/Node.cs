using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryNetwork
{
    public enum NetworkType { Random, QLearning };

    public class Node
    {
        public int id { get; set; } // Numer systemu 
        public int serviceChannelsNumber { get; set; } // ilośc kanałów obsługi (-1 dla nieskończoności)
        public int queueMaxSize { get; set; } // Rozmiar kolejki (-1 dla nieskończoności)
        public int customerCount { get; set; } // Aktualna liczba zgłoszeń
        public int checkoutTime { get; set; } // Czas obsługi dla danej klasy zgłoszenia
        private List<int> timeCounter { get; set; } //Licznik czasu obsługi
        public Dictionary<int, float> nextSystemsValues { get; set; } //Następne systemy i wartości Oczekiwane
        public int customerSum { get; set; }
        public static Random randNode = new Random();

        public Node(int id, int serviceChannelsNumber, int queueMaxSize, int checkoutTime, List<int> systems)
        {
            this.id = id;
            this.serviceChannelsNumber = serviceChannelsNumber;
            this.queueMaxSize = queueMaxSize;
            this.customerCount = 0;
            this.customerSum = 0;
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
                customerSum++;
                timeCounter.Add(checkoutTime);
            }
        }

        public bool infChanel()
        {
            if (serviceChannelsNumber == -1)
                return true;
            return false;
        }

        public bool infQueue()
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
            if (timeCounter.Count > 0)
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
        }

        private void decrementTimerForEachChanel(int chanelNumber)
        {
            int servingClients = (customerCount > chanelNumber) ? chanelNumber : customerCount;
            for (int i = 0; i < servingClients; i++)
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

        public int getNextSystem(NetworkType networkType)
        {
            if (networkType == NetworkType.QLearning)
            {
                KeyValuePair<int, float> max = new KeyValuePair<int, float>(0, Single.MinValue);

                foreach (var sys in nextSystemsValues)
                {
                    if (sys.Value > max.Value)
                        max = sys;
                }
                return max.Key;
            }
            else if (networkType == NetworkType.Random)
            {
                
                int nexNode = randNode.Next(0, nextSystemsValues.Count);
                return nextSystemsValues.ElementAt(nexNode).Key;
            }
            else
            {
                return -1;
            }
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
            int timeCounterLength = timeCounter.Count - 1;
            for(int i= timeCounterLength; i>=0;i--)
            {
                if (timeCounter[i] == 0)
                {
                    timeCounter.RemoveAt(i);
                    customerCount--;
                }
            }
        }

        public void updateExpectedValue(int systemNumber, float newValue)
        {
            nextSystemsValues[systemNumber] = newValue;
        }

        public void resetNode()
        {
            this.customerCount = 0;
            this.customerSum = 0;
            this.timeCounter.Clear();
            
            foreach (var key in nextSystemsValues.Keys.ToList())
            {
                nextSystemsValues[key] = 0;
            }
        }
    }
}
