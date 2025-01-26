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
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            string dataDirectory = Path.Combine(baseDirectory, "Data");
            string filePathKlienci = Path.Combine(dataDirectory, "klienci.json");
            string filePathFilmy = Path.Combine(dataDirectory, "filmy.json");
            string filePathWypozyczenia = Path.Combine(dataDirectory, "wypozyczenia.json");

            if (!Directory.Exists(dataDirectory))
            {
                Console.WriteLine($"Katalog {dataDirectory} nie istnieje.");
                return;
            }

            Wypozyczalnia wypozyczalnia = new Wypozyczalnia();

            bool wyjscie = false;
            Console.WriteLine(OdczytZapis.OdczytajzPliku(filePathFilmy, filePathKlienci, filePathWypozyczenia));
            do
            {
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
                Console.WriteLine("9. Zapisz się do obserwowania filmu");
                Console.WriteLine("10. Wypisz się z obserwowania filmu");
                Console.WriteLine("11. Wyjście");
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
                        if (Wypozyczalnia.filmy.Count == 0)
                        {
                            Console.WriteLine("Brak filmow");
                        }
                        foreach (var film in Wypozyczalnia.filmy)
                        {
                            if (film.dostepnosc == false)
                                Console.WriteLine($"ID: {film.id}, Tytuł: {film.tytul}, Rok: {film.rok_pr}, Gatunek: {film.gatunek}, Dostępność: Niedostępny");
                            else
                                Console.WriteLine($"ID: {film.id}, Tytuł: {film.tytul}, Rok: {film.rok_pr}, Gatunek: {film.gatunek}, Dostępność: Dostępny");
                        }
                        break;
                    case 4:
                        if (Wypozyczalnia.klienci.Count == 0)
                        {
                            Console.WriteLine("Brak klientow");
                        }
                        foreach (var klient in Wypozyczalnia.klienci)
                        {
                            Console.WriteLine($"ID: {klient.id}, Imię: {klient.imie}, Nazwisko: {klient.nazwisko}, Mail: {klient.mail}, Telefon: {klient.telefon}, Max wypożyczeń: {klient.max_wyp}, Aktualnie wypożeczeń: {klient.akt_wyp}");

                        }
                        break;
                    case 5:
                        Console.WriteLine("Dodawanie nowego filmu:");
                        Console.Write("Tytuł: ");
                        string tytul = Console.ReadLine();
                        Console.Write("Rok produkcji: ");
                        int rok_pr = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Gatunek:");
                        string gatunek = Console.ReadLine();
                        wypozyczalnia.dodajFilm(tytul, rok_pr, gatunek);
                        Console.WriteLine("Dodano film");
                        break;
                    case 6:
                        Console.WriteLine("Dodawanie nowego klienta:");
                        Console.Write("Imię:");
                        string imie = Console.ReadLine();
                        Console.Write("Nazwisko:");
                        string nazwisko = Console.ReadLine();
                        Console.Write("Mail:");
                        string mail = Console.ReadLine();
                        Console.Write("Telefon:");
                        string telefon = Console.ReadLine();
                        wypozyczalnia.dodajKlient(imie, nazwisko, mail, telefon);
                        Console.WriteLine("Dodano klienta");
                        break;
                    case 7:
                        Console.Write("Wpisz swoje ID klienta:");
                        int idKlientView = Convert.ToInt16(Console.ReadLine());
                        Klient klientView = Wypozyczalnia.klienci[idKlientView - 1];

                        var wypozyczeniaKlientaView = Wypozyczalnia.wypozyczenia.Where(w => w.klient.id == idKlientView).ToList();

                        if (klientView == null)
                        {
                            Console.WriteLine("Nie znaleziono klienta o podanym ID.");
                            break;
                        }

                        else if (klientView.akt_wyp == 0)
                        {
                            Console.WriteLine("\nKlient nie ma aktualnie wypożyczonych filmów.");
                            break;
                        }
                        else
                        {
                            Console.WriteLine($"Wypożyczenia klienta {klientView.imie} {klientView.nazwisko}:");
                            Wypozyczenie.WyswietlAktWypozyczeniaUsera(idKlientView);
                            foreach (var wypo in wypozyczeniaKlientaView)
                            {
                                if (wypo.DataZwrotu == null)
                                    Console.WriteLine("[NIEZWRÓCONY] " + wypo.film.tytul + " Data wypożyczenia: " + wypo.DataWypozyczenia);
                            }
                        }

                        break;
                    case 8:
                        Console.Write("Wpisz swoje ID klienta:");
                        int idKlientView2 = Convert.ToInt16(Console.ReadLine());
                        Klient klientView2 = Wypozyczalnia.klienci[idKlientView2 - 1];

                        var wypozyczeniaKlientaView2 = Wypozyczalnia.wypozyczenia.Where(w => w.klient.id == idKlientView2).ToList();

                        if (klientView2 == null)
                        {
                            Console.WriteLine("Nie znaleziono klienta o podanym ID.");
                            break;
                        }

                        else
                        {
                            Console.WriteLine($"Historia wypożyczeń klienta {klientView2.imie} {klientView2.nazwisko}:");
                            Wypozyczenie.WyswietlHistorieWypozyczenUsera(idKlientView2);
                            foreach (var wypo in wypozyczeniaKlientaView2)
                            {
                                if (wypo.DataZwrotu != null)
                                    Console.WriteLine(wypo.film.tytul + " Data wypożyczenia: " + wypo.DataWypozyczenia + " Data zwrotu: " + wypo.DataZwrotu);
                            }
                        }
                        break;
                    
                    case 9:
                        Console.Write("Podaj swoje ID klienta: ");
                        int klientIdObs = Convert.ToInt16(Console.ReadLine());
                        Console.Write("Podaj ID filmu do obserwowania: ");
                        int filmIdObs = Convert.ToInt16(Console.ReadLine());

                        if (klientIdObs > 0 && klientIdObs <= Wypozyczalnia.klienci.Count &&
                            filmIdObs > 0 && filmIdObs <= Wypozyczalnia.filmy.Count)
                        {
                            Klient klientObs = Wypozyczalnia.klienci[klientIdObs - 1];
                            Film filmObs = Wypozyczalnia.filmy[filmIdObs - 1];
                            filmObs.DodajObserwatora(klientObs);
                            Console.WriteLine($"Klient {klientObs.imie} {klientObs.nazwisko} został zapisany do obserwowania filmu '{filmObs.tytul}'.");
                        }
                        else
                        {
                            Console.WriteLine("Podano nieprawidłowe ID klienta lub filmu.");
                        }
                        break;

                    case 10:
                        Console.Write("Podaj swoje ID klienta: ");
                        int klientIdOdobs = Convert.ToInt16(Console.ReadLine());
                        Console.Write("Podaj ID filmu do wypisania: ");
                        int filmIdOdobs = Convert.ToInt16(Console.ReadLine());

                        if (klientIdOdobs> 0 && klientIdOdobs <= Wypozyczalnia.klienci.Count &&
                            filmIdOdobs > 0 && filmIdOdobs <= Wypozyczalnia.filmy.Count)
                        {
                            Klient klientOdobs = Wypozyczalnia.klienci[klientIdOdobs- 1];
                            Film filmOdobs = Wypozyczalnia.filmy[filmIdOdobs - 1];
                            filmOdobs.UsunObserwatora(klientOdobs);
                            Console.WriteLine($"Klient {klientOdobs.imie} {klientOdobs.nazwisko} został wypisany z obserwowania filmu '{filmOdobs.tytul}'.");
                        }
                        else
                        {
                            Console.WriteLine("Podano nieprawidłowe ID klienta lub filmu.");
                        }
                        break;

                    case 11:
                        wyjscie = true;
                        Console.WriteLine(OdczytZapis.ZapiszDaneDoPliku(Wypozyczalnia.filmy, Wypozyczalnia.klienci, Wypozyczalnia.wypozyczenia, filePathFilmy, filePathKlienci, filePathWypozyczenia));
                        break;
                }
                OdczytZapis.ZapiszDaneDoPliku(Wypozyczalnia.filmy, Wypozyczalnia.klienci, Wypozyczalnia.wypozyczenia, filePathFilmy, filePathKlienci, filePathWypozyczenia);
                Console.WriteLine("\nNaciśnij dowolny klawisz aby kontynuować...");
                Console.ReadKey();
                Console.Clear();
            } while (!wyjscie);
        }
    }
}
