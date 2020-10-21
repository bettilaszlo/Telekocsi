using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Telekocsi
{
    class Program
    {
        static List<Auto> auto = new List<Auto>();
        static List<Igeny> igeny = new List<Igeny>();
        
        static void Beolvas()
        {
            StreamReader file = new StreamReader("autok.csv");
            file.ReadLine();

            while (!file.EndOfStream)
            {
                string[] adat = file.ReadLine().Split(';');
                auto.Add(new Auto(adat[0], adat[1], adat[2], adat[3], Convert.ToInt32(adat[4])));

            }
            file.Close();

            StreamReader file2 = new StreamReader("igenyek.csv");
            file2.ReadLine();

            while (!file2.EndOfStream)
            {
                string[] i = file2.ReadLine().Split(';');
                igeny.Add(new Igeny(i[0], i[1], i[2], Convert.ToInt32(i[3])));
            }
            file2.Close();
        }
        static void Masodik()
        {
            Console.WriteLine(auto.Count);
        }
        static void Harmadik()
        {
            int ferohelyek = 0;
            foreach (var a in auto)
            {
                if (a.Indulas == "Budapest" && a.Cel == "Miskolc")
                {
                    ferohelyek = ferohelyek + a.Ferohely;
                }
            }
            Console.WriteLine("3. feladat {0}", ferohelyek);
        }
        static void Negyedik()
        {
            //Dictionary<string, int> utvonalak = new Dictionary<string, int>();
            //foreach (var a in auto)
            //{
            //    if (!utvonalak.ContainsKey(a.Utvonal))
            //    {
            //        utvonalak.Add(a.Utvonal, a.Ferohely);
            //    }
            //    else
            //    {
            //        utvonalak[a.Utvonal] = utvonalak[a.Utvonal] + a.Ferohely;
            //    }
            //}

            int max = 0;
            string utv = "";

            //foreach (var u in utvonalak)
            //{
            //    if (u.Value > max)
            //    {
            //        max = u.Value;
            //        utv = u.Key;
            //    } 
            //}
            


            var utvonalak = from a in auto
                            orderby a.Utvonal
                            group a by a.Utvonal into temp
                            select temp;

            foreach (var ut in utvonalak)
            {
                int fh = ut.Sum(x => x.Ferohely);
                if (max < fh)
                {
                    max = fh;
                    utv = ut.Key;
                }
               // Console.WriteLine($"{ut.Key} -> {ut.Count()}");
            }
            Console.WriteLine("4. feladat");
            Console.WriteLine($"{max} - {utv}");
            Console.ReadKey();

        }
        static void Otodik()
        {
            Console.WriteLine("5. feladat");
            //int db = 0;
            foreach (var ig in igeny)
            {
                int i = 0;
                while (i < auto.Count && 
                    !(
                    ig.Indulas == auto[i].Indulas &&  
                    ig.Cel == auto[i].Cel && 
                    ig.Szemelyek <= auto[i].Ferohely))
                {
                    i++;
                }
                if (i < auto.Count)
                {
                    Console.WriteLine($"{ig.Azonosito} => {auto[i].Rendszam}");
                    //db++;
                }
            }
            //Console.WriteLine(db);
        }
        static void Hatodik()
        {
            StreamWriter fajl = new StreamWriter("utasuzenetek.txt");
            foreach (var ig in igeny)
            {
                int i = 0;
                while (i < auto.Count &&
                    !(
                    ig.Indulas == auto[i].Indulas &&
                    ig.Cel == auto[i].Cel &&
                    ig.Szemelyek <= auto[i].Ferohely))
                {
                    i++;
                }
                if (i < auto.Count)
                {
                    Console.WriteLine($"{ig.Azonosito}: Rendszám: {auto[i].Rendszam}, Telefonszám: {auto[i].Telefonszam}");
                }
                else
                {
                    fajl.WriteLine($"{ig.Azonosito}: Sajnos nem sikerült autót találni");
                }
            }
        }
        static void Main(string[] args)
        {
            Beolvas();
            Masodik();
            Harmadik();
            Negyedik();
            Otodik();
            Hatodik();
            Console.ReadKey();
        }
    }
}
