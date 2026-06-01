using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WypozyczalaniaAut.Modele;
using WypozyczalaniaAut.Reguly;

namespace WypozyczalaniaAut.Logika
{
    public class Wypozyczalnia
    {
        private List<Pojazd> _auta = new List<Pojazd>();
        private List<Klient> _klienci = new List<Klient>();
        private List<Wypozyczenie> _historiaWypozyczen = new List<Wypozyczenie>();

        public void DodajAuto(Pojazd p)
        {
            _auta.Add(p);
        }

        public void ZarejestrujKlienta(Klient k)
        {
            _klienci.Add(k);
        }

        public List<Pojazd> SprawdzDostepnosc()
        {
            List<Pojazd> dostepneAuta = new List<Pojazd>();

            foreach (Pojazd auto in _auta)
            {
                if (auto.Status == StatusAuta.Dostepny)
                {
                    dostepneAuta.Add(auto);
                }
            }
            return dostepneAuta;
        }

        public Wypozyczenie ZarejestrujWypozyczenie(Klient k, Pojazd p, IRegulaOplat regula)
        {
            if (p.Status != StatusAuta.Dostepny)
            {
                throw new Exception("Ten pojazd nie jest obecnie dostępny");
            }

            int noweId = _historiaWypozyczen.Count + 1;
            Wypozyczenie noweWypozyczenie = new Wypozyczenie(noweId, p, k, regula);

            p.ZmienStatus(StatusAuta.Wypozyczony);
            _historiaWypozyczen.Add(noweWypozyczenie);

            return noweWypozyczenie;
        }

        public double ZarejestrujZwrot(int idWypozyczenia, DateTime dataOddania)
        {
            Wypozyczenie wypozyczenie = null;
            foreach (var w in _historiaWypozyczen)
            {
                if (w.IdWypozyczenia == idWypozyczenia)
                {
                    wypozyczenie = w;
                    break;
                }
            }
            if (wypozyczenie == null)
            {
                throw new Exception("Nie znaleziono takiego wypożyczenia.");
            }
            if (wypozyczenie.DataZwrotu.HasValue)
            {
                throw new Exception("To auto zostało już zwrócone.");
            }
            wypozyczenie.ZakonczWypozyczenie(dataOddania);
            return wypozyczenie.ObliczCalkowityKoszt();
        }

        public IReadOnlyList<Pojazd> Auta => _auta.AsReadOnly();
        public IReadOnlyList<Klient> Klienci => _klienci.AsReadOnly();
        public IReadOnlyList<Wypozyczenie> Historia => _historiaWypozyczen.AsReadOnly();
    }
}
