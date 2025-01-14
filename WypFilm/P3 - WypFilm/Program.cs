using Microsoft.VisualBasic;
using System.Runtime.CompilerServices;
using System;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Wypozyczalnia
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            Wypozyczalnia wypozyczalnia = new Wypozyczalnia();
            bool wyjscie = false;
            do {
                Console.WriteLine();
                Console.WriteLine("Witaj w Wypożyczalni Filmów!");
                Console.WriteLine("1. Wypozyczenie filmu");
                Console.WriteLine("2. Zwrot filmu");
                Console.WriteLine("3. Wyświetl wszystkie filmy");
                Console.WriteLine("4. Wyświetl klientów");
                Console.WriteLine("5. Dodaj film");
                Console.WriteLine("6. Dodaj klienta");
                Console.WriteLine("7. Wyświetl swoje akt. wypożyczenia");
                Console.WriteLine("8. Wyświetl historię swoich wypożyczeń");
                Console.WriteLine("9. Wyjście");
                Console.Write("Wybierz opcje: ");
                int wybor = Convert.ToInt16(Console.ReadLine());
                Console.WriteLine("");
                switch (wybor)
                {
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        break;
                    case 7:
                        break;
                    case 8:
                        break;
                    case 9:
                        break;
                }
            } while (!wyjscie);
        }

        

    }
}
