using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WypozyczalaniaAut.Reguly
{
    public interface IRegulaOplat
    {
        double ObliczKoszt(DateTime dataWypoz, DateTime dataZwrotu, double stawka);
    }
}
