using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Wypozyczalnia.Tests
{
    [TestFixture]
    public class WypozyczenieTests
    {
        [Test]
        public void RejestracjaWypozyczenia_ValidData_ReturnsSuccessMessage()
        {
            // stworzenie przykładowego klienta oraz filmu (setup)
            Wypozyczalnia.filmy = new List<Film>
            {
                new Film(1, 2022, "Testowy Film", "Komedia", true),
            };

            Wypozyczalnia.klienci = new List<Klient>
            {
                new Klient(1, "Jan", "Kowalski", "jan.kowalski@example.com", "123456789", 3),
            };

            Wypozyczalnia.wypozyczenia = new List<Wypozyczenie>();
            // arrange
            int idKlient = 1;
            int idFilm = 1;

            // act
            string wynik = Wypozyczenie.RejestracjaWypozyczenia(idKlient, idFilm);

            // assert
            Assert.That(wynik, Is.EqualTo("\nPomyślnie wypożyczono film 'Testowy Film'"));
            Assert.That(Wypozyczalnia.filmy[0].dostepnosc, Is.False);
            Assert.That(Wypozyczalnia.klienci[0].akt_wyp, Is.EqualTo(1));
            Assert.That(Wypozyczalnia.wypozyczenia.Count, Is.EqualTo(1));

            // weryfikacja danych klienta
            var sprawdzanyKlient = Wypozyczalnia.klienci[0];
            Assert.That(sprawdzanyKlient, Is.Not.Null);
            Assert.That(sprawdzanyKlient.imie, Is.EqualTo("Jan"));
            Assert.That(sprawdzanyKlient.nazwisko, Is.EqualTo("Kowalski"));
            Assert.That(sprawdzanyKlient.mail, Is.EqualTo("jan.kowalski@example.com"));
            Assert.That(sprawdzanyKlient.telefon, Is.EqualTo("123456789"));
        }
    }
}
