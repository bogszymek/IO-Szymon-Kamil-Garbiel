using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Wypozyczalnia
{
    internal class Wypozyczenie
    {
        public Film film;
        public Klient klient;
        public DateTime DataWypozyczenia;
        public DateTime? DataZwrotu;
        public Wypozyczenie( Film film, Klient klient)
        {
            this.film = film;
            this.klient = klient;
            DataWypozyczenia = DateTime.Now;
            DataZwrotu = null;
        }

        public static string RejestracjaWypozyczenia(int idKlient, int idFilm)
        {
            Film film = Wypozyczalnia.filmy[idFilm - 1];
            Klient klient = Wypozyczalnia.klienci[idKlient - 1];

            if (klient.akt_wyp >= klient.max_wyp)
            {
                return "Klient osiągnął limit wypożyczeń.";

            }

            if (!film.dostepnosc)
            {
                return "Film nie jest dostępny.";

            }

            film.dostepnosc = false;
            klient.akt_wyp++;
            Wypozyczenie noweWypozyczenie = new Wypozyczenie(film, klient);
            Wypozyczalnia.wypozyczenia.Add(noweWypozyczenie);
            return $"\nPomyślnie wypożyczono film '{film.tytul}'";
        }
        public static string RejestracjaZwrotu(int idKlient, int idZw)
        {
            Klient klient = Wypozyczalnia.klienci[idKlient - 1];
            var wypozyczeniaKlienta = Wypozyczalnia.wypozyczenia.Where(w => w.klient.id == idKlient && w.DataZwrotu == null).ToList();

            Wypozyczenie wypoDoZwrotu = wypozyczeniaKlienta.FirstOrDefault(w => w.film.id == idZw);
            if (wypoDoZwrotu != null)
            {
                Film film = Wypozyczalnia.filmy[idZw - 1];
                if (film != null)
                {
                    film.dostepnosc = true;
                }
                wypoDoZwrotu.DataZwrotu = DateTime.Now;

                klient.akt_wyp--;

                return ($"Film '{film.tytul}' został zwrócony.");
            }
            else return "\nNie wypożyczono tego filmu!";
        }

        public static List<Wypozyczenie> WyswietlAktWypozyczeniaUsera(int idKlient)
        {
            Klient klient = Wypozyczalnia.klienci[idKlient - 1];
            var wypozyczeniaKlienta = Wypozyczalnia.wypozyczenia.Where(w => w.klient.id == idKlient).ToList();
            List<Wypozyczenie> nieZw = new List<Wypozyczenie>();
            foreach (var wypo in wypozyczeniaKlienta)
            {
                if (wypo.DataZwrotu == null)
                    nieZw.Add(wypo);
            }
            return nieZw;
        }

        public static List<Wypozyczenie> WyswietlHistorieWypozyczenUsera(int idKlient)
        {
            Klient klient = Wypozyczalnia.klienci[idKlient - 1];

            var wypozyczeniaKlienta = Wypozyczalnia.wypozyczenia.Where(w => w.klient.id == idKlient && w.DataZwrotu != null).ToList();

            List<Wypozyczenie> historia = new List<Wypozyczenie>();
            foreach (var wypo in wypozyczeniaKlienta)
            {
                if (wypo.DataZwrotu != null)
                    historia.Add(wypo);
            }
            return historia;
        }
    }
}
