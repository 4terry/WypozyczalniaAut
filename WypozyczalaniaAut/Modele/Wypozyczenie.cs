using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WypozyczalaniaAut.Reguly;

namespace WypozyczalaniaAut.Modele
{
    public class Wypozyczenie
    {
        public int IdWypozyczenia { get; private set; }
        public Pojazd WypozyczoneAuto { get; private set; }
        public Klient Klient { get; private set; }
        public DateTime DataWypozyczenia { get; private set; }
        public DateTime? DataZwrotu { get; private set; }
        public IRegulaOplat RegulaOplat { get; private set; }

        public Wypozyczenie(int id, Pojazd auto, Klient klient, IRegulaOplat regula)
        {
            IdWypozyczenia = id;
            WypozyczoneAuto = auto;
            Klient = klient;
            RegulaOplat = regula;
            DataWypozyczenia = DateTime.Now;
            DataZwrotu = null;
        }

        public void ZakonczWypozyczenie(DateTime dataOddania)
        {
            if (DataZwrotu.HasValue)
            {
                throw new Exception("To auto zostało już zwrócone");
            }
            if (dataOddania < DataWypozyczenia)
            {
                throw new Exception("Data zwrotu nie może być wcześniejsza niż data wypożyczenia");
            }

            DataZwrotu = dataOddania;
            WypozyczoneAuto.ZmienStatus(StatusAuta.Dostepny);
        }

        public double ObliczCalkowityKoszt()
        {
            if (!DataZwrotu.HasValue)
            {
                return 0;
            }
            double kosztPodstawowy = RegulaOplat.ObliczKoszt(DataWypozyczenia, DataZwrotu.Value, WypozyczoneAuto.StawkaBazowa);
            double kosztyDodatkowe = WypozyczoneAuto.ObliczKosztyDodatkowe();
            return kosztPodstawowy + kosztyDodatkowe;
        }

        public override string ToString()
        {
            string status;
            if (DataZwrotu.HasValue)
            {
                status = $"Zwrócono ({DataZwrotu.Value.ToShortDateString()})";
            }
            else
            {
                status = "Wypożyczone";
            }
            return $"Wynajem nr.{IdWypozyczenia}: {Klient.ImieNazwisko} wynajął {WypozyczoneAuto.Model} [{status}]";
        }
    }
}
