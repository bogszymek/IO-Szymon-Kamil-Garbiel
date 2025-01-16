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
        public void zmianaDost()
        {
            dostepnosc = !dostepnosc;
        }

        public Film(int id, int rok_pr, string tytul, string gatunek, bool dostepnosc)
        {
            this.id = id;
            this.rok_pr = rok_pr;
            this.tytul = tytul;
            this.gatunek = gatunek;
            this.dostepnosc = dostepnosc;
        }
    }
}
