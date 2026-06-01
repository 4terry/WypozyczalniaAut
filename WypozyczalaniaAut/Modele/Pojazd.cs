using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WypozyczalaniaAut.Modele
{
    public abstract class Pojazd
    {
        public string VIN { get; protected set; }
        public string Model { get; protected set; }
        public StatusAuta Status { get; protected set; }
        public double StawkaBazowa { get; protected set; }

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
            return $"{Model} [VIN: {VIN}] - {Status} | Stawka bazowa: {StawkaBazowa} PLN";
        }
    }
}
