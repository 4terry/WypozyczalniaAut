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
            return _auta.Where(a => a.Status == StatusAuta.Dostepny).ToList();
        }

        public Wypozyczenie ZarejestrujWypozyczenie(Klient k, Pojazd p, IRegulaOplat regula)
        {
            if (p.Status != StatusAuta.Dostepny)
            {
                throw new Exception("Błąd: Ten pojazd nie jest obecnie dostępny!");
            }

            int noweId = _historiaWypozyczen.Count + 1;
            Wypozyczenie noweWypozyczenie = new Wypozyczenie(noweId, p, k, regula);

            p.ZmienStatus(StatusAuta.Wypozyczony);
            _historiaWypozyczen.Add(noweWypozyczenie);

            return noweWypozyczenie;
        }

        public double ZarejestrujZwrot(int idWypozyczenia)
        {
            var wypozyczenie = _historiaWypozyczen.FirstOrDefault(w => w.IdWypozyczenia == idWypozyczenia);

            if (wypozyczenie == null)
                throw new Exception("Błąd: Nie znaleziono takiego wypożyczenia.");

            if (wypozyczenie.DataZwrotu.HasValue)
                throw new Exception("Błąd: To auto zostało już zwrócone.");

            wypozyczenie.ZakonczWypozyczenie(DateTime.Now);

            wypozyczenie.WypozyczoneAuto.ZmienStatus(StatusAuta.Dostepny);

            return wypozyczenie.ObliczCalkowityKoszt();
        }

        public IReadOnlyList<Pojazd> Auta => _auta.AsReadOnly();
        public IReadOnlyList<Klient> Klienci => _klienci.AsReadOnly();
        public IReadOnlyList<Wypozyczenie> Historia => _historiaWypozyczen.AsReadOnly();
    }
}
