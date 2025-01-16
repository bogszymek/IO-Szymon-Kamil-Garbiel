using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Wypozyczalnia.Tests
{
    [TestFixture]
    public class OdczytZapisTests
    {
        [Test]
        public void ZapiszDaneDoPliku_Should_Save_Single_Client_Correctly()
        {
            //arrange
            string tempFilePathKlienci = Path.GetTempFileName();

            var klienci = new List<Klient>
            {
                new Klient(1, "TestImie", "TestNazwisko", "test@gmail.com", "123123123", 5)
            };

            var filmy = new List<Film>(); //pusta lista filmow
            var wypozyczenia = new List<Wypozyczenie>(); //pusta lista wypozyczen

            //act
            OdczytZapis.ZapiszDaneDoPliku(filmy, klienci, wypozyczenia, "filmy.json", tempFilePathKlienci, "wypozyczenia.json");

            string jsonFromFile = System.IO.File.ReadAllText(tempFilePathKlienci);
            var deserializedKlienci = JsonConvert.DeserializeObject<List<Klient>>(jsonFromFile);

            //pierwszy klient z listy
            var savedClient = deserializedKlienci[0];

            //assert (ignorujemy id, max_wyp, akt_wyp)
            Assert.That(savedClient, Is.Not.Null);
            Assert.That(savedClient.imie, Is.EqualTo("TestImie"));
            Assert.That(savedClient.nazwisko, Is.EqualTo("TestNazwisko"));
            Assert.That(savedClient.mail, Is.EqualTo("test@gmail.com"));
            Assert.That(savedClient.telefon, Is.EqualTo("123123123"));


            //usuniecie pliku tymczasowego
            if (System.IO.File.Exists(tempFilePathKlienci))
            {
                System.IO.File.Delete(tempFilePathKlienci);
            }
        }
    }
}
