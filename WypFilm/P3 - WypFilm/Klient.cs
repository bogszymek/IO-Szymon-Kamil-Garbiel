using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Wypozyczalnia
{
    public class Klient : IObserver
    {
        public void Powiadom(string wiadomosc)
        {
            Console.WriteLine($"[Powiadomienie dla {imie} {nazwisko}]: {wiadomosc}");
        }

        public int id, max_wyp;
        public int akt_wyp = 0;
        public string imie, nazwisko, mail, telefon;
        


        public Klient(int id, string imie, string nazwisko, string mail, string telefon, int max_wyp)
        {
            this.id = id;
            this.imie = imie;
            this.nazwisko = nazwisko;
            this.mail = mail;
            this.telefon = telefon;
            this.max_wyp = max_wyp;
        }
    }
}
