using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Wypozyczalnia
{
    // Interfejs strategii dla operacji na plikach
    public interface IStrategyPlik
    {
        void Zapisz<T>(List<T> dane, string sciezkaPliku);
        List<T> Odczytaj<T>(string sciezkaPliku);
    }

    // Implementacja strategii dla JSON
    public class JsonPlikStrategy : IStrategyPlik
    {
        public void Zapisz<T>(List<T> dane, string sciezkaPliku)
        {
            string json = JsonConvert.SerializeObject(dane, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            File.WriteAllText(sciezkaPliku, json);
        }

        public List<T> Odczytaj<T>(string sciezkaPliku)
        {
            if (!File.Exists(sciezkaPliku))
            {
                Console.WriteLine($"Plik {sciezkaPliku} nie istnieje");
                return new List<T>();
            }

            string json = File.ReadAllText(sciezkaPliku);
            return JsonConvert.DeserializeObject<List<T>>(json) ?? new List<T>();
        }
    }

    // Klasa zarządzająca zapisem i odczytem przy użyciu strategii
    public class OdczytZapis
    {
        private static IStrategyPlik _strategy = new JsonPlikStrategy();

        public static void ZmienStrategie(IStrategyPlik nowaStrategia)
        {
            _strategy = nowaStrategia;
        }

        public static void ZapiszDane(string fileFilmy, string fileKlienci, string fileWypozyczenia)
        {
            _strategy.Zapisz(Wypozyczalnia.filmy, fileFilmy);
            _strategy.Zapisz(Wypozyczalnia.klienci, fileKlienci);
            _strategy.Zapisz(Wypozyczalnia.wypozyczenia, fileWypozyczenia);
        }

        public static void OdczytajDane(string fileFilmy, string fileKlienci, string fileWypozyczenia)
        {
            Wypozyczalnia.filmy = _strategy.Odczytaj<Film>(fileFilmy);
            Wypozyczalnia.klienci = _strategy.Odczytaj<Klient>(fileKlienci);
            Wypozyczalnia.wypozyczenia = _strategy.Odczytaj<Wypozyczenie>(fileWypozyczenia);
        }
    }
}