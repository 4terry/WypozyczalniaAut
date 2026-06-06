using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WypozyczalaniaAut.Reguly
{
    public class RegulaGodzinowa : IRegulaOplat
    {
        public double ObliczKoszt(DateTime dataWypoz, DateTime dataZwrotu, double stawka)
        {
            double godziny = Math.Ceiling((dataZwrotu - dataWypoz).TotalHours);
            if (godziny <= 0)
            {
                godziny = 1;
            }
            return godziny * (stawka / 24.0);
        }
    }
}
