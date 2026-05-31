using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WypozyczalaniaAut.Modele
{
    public class Klient
    {
        public int IdKlienta { get; private set; }
        public string ImieNazwisko { get; private set; }
        public Klient(int idKlienta, string imieNazwisko)
        {
            IdKlienta = idKlienta;
            ImieNazwisko = imieNazwisko;
        }

        public int GetId()
        {
            return IdKlienta;
        }

        public override string ToString()
        {
            return $"id:{IdKlienta} {ImieNazwisko}";
        }
    }
}
