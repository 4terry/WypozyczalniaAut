using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WypozyczalaniaAut.Logika;

namespace WypozyczalaniaAut.Dane
{
    public interface IRepozytoriumDanych
    {
        void ZapiszDane(Wypozyczalnia wypozyczalnia);
        Wypozyczalnia WczytajDane();
    }
}
