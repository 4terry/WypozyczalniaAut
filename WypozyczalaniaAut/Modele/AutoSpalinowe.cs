using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WypozyczalaniaAut.Modele
{
    public class AutoSpalinowe : Pojazd
    {
        public double Paliwo { get; private set; }
        private const double PojemnoscBaku = 50.0;

        public AutoSpalinowe(string vin, string model, double stawkaBazowa)
            : base(vin, model, stawkaBazowa)
        {
            Paliwo = PojemnoscBaku;
        }

        public void ZaktualizujPaliwo(double aktualnyStan)
        {
            if (aktualnyStan < 0 || aktualnyStan > PojemnoscBaku)
            {
                throw new ArgumentException("Nieprawidłowy stan paliwa!");
            }
            Paliwo = aktualnyStan;
        }

        public override double ObliczKosztyDodatkowe()
        {
            double BrakujacePaliwo = PojemnoscBaku - Paliwo;
            return BrakujacePaliwo * 10.0;
        }

        public override string ToString()
        {
            return base.ToString() + $" [Spalinowe, Paliwo: {Paliwo}L]";
        }
    }
}
