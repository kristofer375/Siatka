using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Siatka
{
    class Txt
    {
        string plik = @"./wynik.txt";
        List<string> wiersze;

        public void Odczyt()
        {
            wiersze = File.ReadAllLines(plik).ToList();
        }
        public void Dodaj(string x)
        {
            wiersze.Add(x + " " + DateTime.Now.ToString());
        }
        public void Zapisz()
        {
            File.WriteAllLines(plik, wiersze);
        }
    }
}
