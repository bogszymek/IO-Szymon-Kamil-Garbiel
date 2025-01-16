using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wypozyczalnia
{
    public class OdczytZapis

    {
        public static string OdczytajzPliku(string filePatchFilmy, string filePatchKlienci, string filePatchWypozyczenia)
        {
            if (!File.Exists(filePatchFilmy))
            {
                Console.WriteLine("Plik filmy nie istnieje"); 
                
            }
            if (!File.Exists(filePatchKlienci))
            {
                Console.WriteLine("Plik klienci nie istnieje");
            }
            if (!File.Exists(filePatchWypozyczenia))
            {
                Console.WriteLine("Plik wypozyczenia nie istnieje");

            }

            string jsonDaneFilmy = File.ReadAllText(filePatchFilmy);
            string jsonDaneKlienci = File.ReadAllText(filePatchKlienci);
            string jsonDaneWypozyczenia = File.ReadAllText(filePatchWypozyczenia);

            Wypozyczalnia.klienci = JsonConvert.DeserializeObject<List<Klient>>(jsonDaneKlienci);
            Wypozyczalnia.filmy = JsonConvert.DeserializeObject<List<Film>>(jsonDaneFilmy);
            Wypozyczalnia.wypozyczenia = JsonConvert.DeserializeObject<List<Wypozyczenie>>(jsonDaneWypozyczenia);

            return "Pomyślnie wczytano dane z plików";
        }

        public static string ZapiszDaneDoPliku(List<Film> filmy, List<Klient> klienci, List<Wypozyczenie> wypozyczenia, string filePatchFilmy, string filePatchKlienci, string filePatchWypozyczenia)
        {
            
            string jsonFilmy = JsonConvert.SerializeObject(filmy);
            string jsonKlienci = JsonConvert.SerializeObject(klienci, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            string jsonWypozyczenia = JsonConvert.SerializeObject(wypozyczenia, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            File.WriteAllText(filePatchFilmy, jsonFilmy);
            File.WriteAllText(filePatchKlienci, jsonKlienci);
            File.WriteAllText(filePatchWypozyczenia, jsonWypozyczenia);

            return "Pomyślnie zapisano dane do plików";
            
        }
    }
}
