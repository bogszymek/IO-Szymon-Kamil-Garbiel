using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wypozyczalnia
{
    public class Film
    {
        public int id, rok_pr;
        public string tytul, gatunek;
        public bool dostepnosc;

        private List<IObserver> obserwatorzy = new List<IObserver>();

        public Film(int id, int rok_pr, string tytul, string gatunek, bool dostepnosc)
        {
            this.id = id;
            this.rok_pr = rok_pr;
            this.tytul = tytul;
            this.gatunek = gatunek;
            this.dostepnosc = dostepnosc;
        }
        public void zmianaDost()
        {
            dostepnosc = !dostepnosc;
            PowiadomObserwatorow();
        }
        public void DodajObserwatora(IObserver obserwator)
        {
            obserwatorzy.Add(obserwator);
        }
        public void UsunObserwatora(IObserver obserwator)
        {
            obserwatorzy.Remove(obserwator);
        }
        private void PowiadomObserwatorow()
        {
            foreach (var obserwator in obserwatorzy)
            {
                obserwator.Powiadom($"Film '{tytul}' jest teraz {(dostepnosc ? "dostępny" : "niedostępny")}.");
            }
        }
    }
}
