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
                        Console.Write("Wpisz swoje ID klienta:");
                        int idKlientWyp = Convert.ToInt16(Console.ReadLine());
                        Console.WriteLine("\nWybierz film do wypożyczena: ");
                        foreach (var film in Wypozyczalnia.filmy)
                        {
                            if (film.dostepnosc == true)
                            {
                                Console.WriteLine($"ID: {film.id}, Tytuł: {film.tytul}, Rok: {film.rok_pr}, Gatunek: {film.gatunek}");
                            }
                        }
                        Console.Write("\nWybierz ID filmu do wypożyczenia: ");
                        int idFilm = Convert.ToInt16(Console.ReadLine());
                        Console.WriteLine(Wypozyczenie.RejestracjaWypozyczenia(idKlientWyp, idFilm));

                        break;
                    case 2:
                        Console.Write("Wpisz swoje ID klienta:");
                        int idKlientZw = Convert.ToInt16(Console.ReadLine());
                        Klient klientZw = Wypozyczalnia.klienci[idKlientZw - 1];
                        var wypozyczeniaKlienta = Wypozyczalnia.wypozyczenia.Where(w => w.klient.id == idKlientZw && w.DataZwrotu == null).ToList();
                        if (wypozyczeniaKlienta.Count == 0 && klientZw.akt_wyp == 0)
                        {
                            Console.WriteLine("\nBrak wypożyczonych filmów!");
                        }
                        else
                        {
                            foreach (var wypo in wypozyczeniaKlienta)
                            {
                                Console.WriteLine($"{wypo.film.id}. {wypo.film.tytul}");
                            }
                            Console.Write("Podaj ID filmu który chcesz zwrócić: ");
                            int idZw = Convert.ToInt16(Console.ReadLine());
                            Console.WriteLine(Wypozyczenie.RejestracjaZwrotu(idKlientZw, idZw));
                        }
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
