using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WypozyczalaniaAut.Modele
{
    public class AutoElektryczne : Pojazd
    {
        public double PoziomBaterii { get; set; }

        public AutoElektryczne(string vin, string model, double stawkaBazowa)
            : base(vin, model, stawkaBazowa)
        {
            PoziomBaterii = 100.0;
        }

        public override double ObliczKosztyDodatkowe()
        {
            if (PoziomBaterii < 20.0) return 50.0;
            return 0.0;
        }

        public override string ToString()
        {
            return base.ToString() + " [Elektryczne]";
        }
    }
}
