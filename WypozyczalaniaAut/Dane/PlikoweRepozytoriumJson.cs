using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WypozyczalaniaAut.Logika;
using WypozyczalaniaAut.Modele;

namespace WypozyczalaniaAut.Dane
{
    public class PlikoweRepozytoriumJson : IRepozytoriumDanych
    {
        private string _sciezkaDoPliku;

        public PlikoweRepozytoriumJson(string sciezkaDoPliku = "historia_wypozyczen.json")
        {
            _sciezkaDoPliku = sciezkaDoPliku;
        }

        public void ZapiszDane(Wypozyczalnia wypozyczalnia)
        {
            List<WypozyczenieTemp> daneDoZapisu = new List<WypozyczenieTemp>();

            foreach (Wypozyczenie w in wypozyczalnia.Historia)
            {
                WypozyczenieTemp noweDane = new WypozyczenieTemp
                {
                    Id = w.IdWypozyczenia,
                    Klient = w.Klient.ImieNazwisko,
                    Auto = w.WypozyczoneAuto.Model,
                    DataWypozyczenia = w.DataWypozyczenia,
                    DataZwrotu = w.DataZwrotu,
                    Status = w.DataZwrotu.HasValue ? "zakończone" : "w trakcie",
                    CalkowityKoszt = w.DataZwrotu.HasValue ? w.ObliczCalkowityKoszt() : 0.0
                };
                daneDoZapisu.Add(noweDane);
            }

            var opcje = new JsonSerializerOptions { WriteIndented = true };
            string jsonTekst = JsonSerializer.Serialize(daneDoZapisu, opcje);
            File.WriteAllText(_sciezkaDoPliku, jsonTekst);
        }

        public Wypozyczalnia WczytajDane()
        {
            Wypozyczalnia wypozyczalnia = new Wypozyczalnia();

            if (File.Exists("klienci.txt"))
            {
                string[] linieKlienci = File.ReadAllLines("klienci.txt");
                foreach (string linia in linieKlienci)
                {
                    string[] dane = linia.Split(';');
                    if (dane.Length >= 2)
                    {
                        int id = int.Parse(dane[0]);
                        string imie = dane[1];
                        wypozyczalnia.ZarejestrujKlienta(new Klient(id, imie));
                    }
                }
            }

            // Wczytywanie aut z pliku tekstowego
            if (File.Exists("auta.txt"))
            {
                string[] linieAuta = File.ReadAllLines("auta.txt");
                foreach (string linia in linieAuta)
                {
                    string[] dane = linia.Split(';');
                    if (dane.Length >= 4)
                    {
                        string typ = dane[0];
                        string vin = dane[1];
                        string model = dane[2];
                        double stawka = double.Parse(dane[3]);

                        if (typ == "Spalinowe")
                        {
                            wypozyczalnia.DodajAuto(new AutoSpalinowe(vin, model, stawka));
                        }
                        else if (typ == "Elektryczne")
                        {
                            wypozyczalnia.DodajAuto(new AutoElektryczne(vin, model, stawka));
                        }
                    }
                }
            }

            return wypozyczalnia;
        }
    }
}
