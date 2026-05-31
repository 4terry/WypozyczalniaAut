using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WypozyczalaniaAut.Modele
{
    public abstract class Pojazd
    {
        public string VIN { get; private set; }
        public string Model { get; private set; }
        public StatusAuta Status { get; private set; }
        public double StawkaBazowa { get; private set; }

        protected Pojazd(string vin, string model, double stawkaBazowa)
        {
            VIN = vin;
            Model = model;
            Status = StatusAuta.Dostepny;
            StawkaBazowa = stawkaBazowa;
        }

        public void ZmienStatus(StatusAuta nowyStatus)
        {
            Status = nowyStatus;
        }

        public abstract double ObliczKosztyDodatkowe();

        public override string ToString()
        {
            return $"{Model} (VIN: {VIN}) - {Status}";
        }
    }
}
