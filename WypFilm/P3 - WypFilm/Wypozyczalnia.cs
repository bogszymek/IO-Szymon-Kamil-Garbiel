using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wypozyczalnia
{
    public class Wypozyczalnia
    {
       
        public static List<Klient> klienci = new List<Klient>();  
        public static List<Film> filmy = new List<Film>();
        public static List<Wypozyczenie> wypozyczenia = new List<Wypozyczenie>();

        public void dodajFilm(string tytul, int rok_pr, string gatunek)
        {
            Film nowy = FilmFactory.StworzFilm(filmy.Count + 1, rok_pr, tytul, gatunek);
            filmy.Add(nowy);
        }

        public void dodajKlient(string imie, string nazwisko, string mail, string telefon)
        {
            Klient nowy = new Klient(klienci.Count + 1, imie, nazwisko, mail, telefon, 3);
            klienci.Add(nowy);
        }
    }
}
