using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Helsinki2017
{
    class Program
    {
        static List<Korcsolya> rovidProgList = new List<Korcsolya>();
        static List<Korcsolya> dontoList = new List<Korcsolya>();

        static void Main(string[] args)
        {
            #region 1. feladat
            StreamReader Olvas = new StreamReader("rovidprogram.csv", Encoding.Default);
            string Fejlec = Olvas.ReadLine();
            while (!Olvas.EndOfStream)
            {
                rovidProgList.Add(new Korcsolya(Olvas.ReadLine()));
            }
            Olvas.Close();
            StreamReader Olvas2 = new StreamReader("donto.csv", Encoding.Default);
            Fejlec = Olvas2.ReadLine();
            while (!Olvas2.EndOfStream)
            {
                dontoList.Add(new Korcsolya(Olvas2.ReadLine()));
            }
            Olvas2.Close();
            #endregion

            #region 2. feladat
            Console.WriteLine("2. feladat");
            Console.WriteLine($"\tA rovidprogramban {rovidProgList.Count} indulo volt");
            #endregion

            #region 3. feladat
            Console.WriteLine("3. feladat");
            bool BejutottE = false;
            for (int i = 0; i < dontoList.Count; i++)
            {
                if (dontoList[i].Orszag == "HUN")
                {
                    BejutottE = true;
                }
            }
            if (BejutottE == true)
            {
                Console.WriteLine("\tA magyar versenyzo bejutott a kurbe");
            }
            else
            {
                Console.WriteLine("\tA magyar versenyzo nem bejutott a kurbe");
            }
            #endregion

            #region 5. feladat
            Console.WriteLine("5. feladat");
            Console.Write("\tKerem a versenyzo nevet: ");
            string bekertNEv = Console.ReadLine();
            bool voltEilyen = false;
            for (int i = 0; i < rovidProgList.Count; i++)
            {
                if (bekertNEv == rovidProgList[i].Nev)
                {
                    voltEilyen = true;
                }
            }
            if (voltEilyen == false)
            {
                Console.WriteLine("\tIlyen nevu indulo nem volt");
            }
            #endregion

            #region 6. feladat
            Console.WriteLine("6. feladat");
            double osszPont = osszPontSzam(bekertNEv);
            Console.WriteLine($"\tA versenyzo osszpontszama: {osszPont}");
            #endregion

            #region 7. feladat
            Console.WriteLine("7. feladat");
            List<string> OrszagLista = new List<string>();
            for (int i = 0; i < dontoList.Count; i++)
            {
                bool SzerepelE = false;
                for (int j = 0; j < OrszagLista.Count; j++)
                {
                    if (dontoList[i].Orszag == OrszagLista[j])
                    {
                        SzerepelE = true;
                    }
                }
                if (SzerepelE == false)
                {
                    OrszagLista.Add(dontoList[i].Orszag);
                }
            }
            int[] OrszagListaSeged = new int[OrszagLista.Count];
            for (int i = 0; i < dontoList.Count; i++)
            {
                for (int j = 0; j < OrszagLista.Count; j++)
                {
                    if (dontoList[i].Orszag == OrszagLista[j])
                    {
                        OrszagListaSeged[j]++;
                    }
                }
            }
            for (int i = 0; i < OrszagListaSeged.Length; i++)
            {
                if (OrszagListaSeged[i] > 1)
                {
                    Console.WriteLine($"\t{OrszagLista[i]}: {OrszagListaSeged[i]} versenyzo");
                }
            }
            #endregion

            #region Alternativ megoldas 7. feladat
            /*foreach (Korcsolya versenyzo in dontoList)
            {
                if (!OrszagLista.Contains(versenyzo.Orszag))
                {
                    OrszagLista.Add(versenyzo.Orszag);
                }
            }
            foreach (string orszag in OrszagLista)
            {
                int db = 0;
                foreach (Korcsolya versenyzo in dontoList)
                {
                    if (versenyzo.Orszag == orszag)
                    {
                        db++;
                    }
                }
                if (db > 1)
                {
                    Console.WriteLine($"\t{orszag}: {db} versenyzo");
                }
            }*/
            #endregion

            #region 8. feladat
            Console.WriteLine("8. feladat: vegeredmeny.csv");
            StreamWriter Iro = new StreamWriter("vegeredmeny.csv", false, Encoding.Default);
            for (int i = 0; i < dontoList.Count; i++)
            {
                Korcsolya newkori = dontoList[i];
                newkori.osszPont = osszPontSzam(dontoList[i].Nev);
                dontoList[i] = newkori;
            }
            dontoList = dontoList.OrderBy(versenyzo => versenyzo.osszPont).ToList();
            dontoList.Reverse();

            int Helyezes = 1;
            foreach (Korcsolya versenyzo in dontoList)
            {
                Iro.WriteLine($"{Helyezes};{versenyzo.Nev};{versenyzo.Orszag};{versenyzo.osszPont}");
                Helyezes++;
            }
            Iro.Close();
            #endregion
            Console.ReadKey();
        }

        #region 4. feladat
        static double osszPontSzam(string nev)
        {
            double osszPont = 0;
            foreach (Korcsolya versenyzo in rovidProgList)
            {
                if (versenyzo.Nev == nev)
                {
                    osszPont += versenyzo.TechPontszam + versenyzo.KompPontszam - versenyzo.Levonas;
                }
            }

            foreach (Korcsolya versenyzo in dontoList)
            {
                if (versenyzo.Nev == nev)
                {
                    osszPont += versenyzo.TechPontszam + versenyzo.KompPontszam - versenyzo.Levonas;
                }
            }
            return osszPont;
        }
        #endregion
    }
    class Korcsolya
    {
        public string Nev;
        public string Orszag;
        public double TechPontszam;
        public double KompPontszam;
        public int Levonas;
        public double osszPont;

        public Korcsolya(string AdatSor)
        {
            string[] AdatSorElemek = AdatSor.Split(';');
            this.Nev = AdatSorElemek[0];
            this.Orszag = AdatSorElemek[1];
            this.TechPontszam = Convert.ToDouble(AdatSorElemek[2].Replace('.', ','));
            this.KompPontszam = Convert.ToDouble(AdatSorElemek[3].Replace('.', ','));
            this.Levonas = Convert.ToInt32(AdatSorElemek[4]);
        }
    }
}
