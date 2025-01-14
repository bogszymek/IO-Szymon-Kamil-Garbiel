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

    }

}
