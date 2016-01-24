using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryNetwork
{
    class Network
    {
        private List<Node> listOfNodes; // Lista systemów w sieci

    }
}

/*
 * Do sieci wchodzi nowe zgłoszenie o klasie K.
 * Zostaje przydzielone do systemu początkowego We (sztuczny lub wybrany z dostępnych).
 * Zwiększona zostaje aktualna liczba zgłoszeń w danym systemie: +1.
 * Jeśli liczba aktualnych zgłoszeń w tym systemie jest mniejsza od ilości stacji obsługi -> zaczynamy obsługę.
 * Jeśli liczba aktualnych zgłoszeń w tym systemie jest większa od ilości stacji obsługi,
 *      ale mniejsza od sumy ilości stacji obsługi i miejsc w kolejce -> czeka.
 * Jeśli liczba aktualnych zgłoszeń w tym systemie jest większa od sumy ilości stacji obsługi i miejsc w kolejce
 *      -> utrata zgłoszenia (opcjonalne, możemy nie brać pod uwagę takich systemów).
 * W momencie kiedy upłynie czas obsługi danego zgłoszenia, wybieramy z listy następny mozliwy system S
 *      (na podstawie algorytmu optymalizacyjnego) i powtarzamy dla niego procedurę.
 * Zgłoszenie wychodzi z sieci po dotarciu do systemu końcowego Wy.
 * 
 * Zakładamy brak priorytetów, niecierpliwych klientów i indywidualnej obsługi.
*/
