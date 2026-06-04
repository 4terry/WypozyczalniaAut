using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WypozyczalaniaAut.Modele
{
    public class AutoElektryczne : Pojazd
    {
        public double PoziomBaterii { get; private set; }

        public AutoElektryczne(string vin, string model, double stawkaBazowa)
            : base(vin, model, stawkaBazowa)
        {
            PoziomBaterii = 100.0;
        }

        public void ZaktualizujBaterie(double procent)
        {
            if (procent < 0 || procent > 100)
            {
                throw new ArgumentException("Nieprawidlowy stan paliwa");
            }
            PoziomBaterii = procent;
        }

        public override double ObliczKosztyDodatkowe()
        {
            if (PoziomBaterii < 30.0)
            {
                return 100.0;
            }

            return 0.0;
        }

        public override string ToString()
        {
            return base.ToString() + $" [Elektryczne, Bateria: {PoziomBaterii}%]";
        }
    }
}
