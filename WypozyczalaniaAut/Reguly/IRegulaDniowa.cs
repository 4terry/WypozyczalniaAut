using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WypozyczalaniaAut.Reguly
{
    public class RegulaDniowa : IRegulaOplat
    {
        public double ObliczKoszt(DateTime dataWypoz, DateTime dataZwrotu, double stawka)
        {
            double dni = Math.Ceiling((dataZwrotu - dataWypoz).TotalDays);

            if (dni == 0) dni = 1;

            return dni * stawka;
        }
    }
}
