using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WypozyczalaniaAut.Modele
{
    public class AutoSpalinowe : Pojazd
    {
        public double BrakujacePaliwo { get; set; }

        public AutoSpalinowe(string vin, string model, double stawkaBazowa)
            : base(vin, model, stawkaBazowa)
        {
            BrakujacePaliwo = 0;
        }

        public override double ObliczKosztyDodatkowe()
        {
            return BrakujacePaliwo * 10.0;
        }

        public override string ToString()
        {
            return base.ToString() + " [Spalinowe]";
        }
    }
}
