using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telekocsi
{
    class Auto
    {
        public string Indulas { get; private set; }
        public string Cel { get; private set; }
        public string Rendszam { get; private set; }
        public string Telefonszam { get; private set; }
        public int Ferohely { get; private set; }
    }
    public Auto(string indulas, string cel, string rendszam, string telefonszam, int ferohely)
    {
        string[] a =

        this.Indulas = indulas;
        this.Cel = cel;
        this.Rendszam = rendszam;
        this.Telefonszam = telefonszam;
        this.Ferohely = ferohely;
    }
}
