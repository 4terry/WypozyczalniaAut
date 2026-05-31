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

        public void ZakonczWypozyczenie(DateTime dataZwrotu)
        {
            DataZwrotu = dataZwrotu;
        }

        public double ObliczCalkowityKoszt()
        {
            if (!DataZwrotu.HasValue) return 0;

            double kosztPodstawowy = RegulaOplat.ObliczKoszt(DataWypozyczenia, DataZwrotu.Value, WypozyczoneAuto.StawkaBazowa);

            double kosztyDodatkowe = WypozyczoneAuto.ObliczKosztyDodatkowe();

            return kosztPodstawowy + kosztyDodatkowe;
        }

        public override string ToString()
        {
            string status = DataZwrotu.HasValue ? $"Zwrócono ({DataZwrotu.Value.ToShortDateString()})" : "Wypożyczone - w trasie";
            return $"Wynajem #{IdWypozyczenia}: {Klient.ImieNazwisko} wynajął {WypozyczoneAuto.Model} [{status}]";
        }
    }
}
