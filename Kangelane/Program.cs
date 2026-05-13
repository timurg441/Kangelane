using System;
using System.Collections.Generic;
using System.IO;

// klass 3: Program
class Program
{
    // Staatiline list kõigi kangelaste hoidmiseks
    static List<Kangelane> kangelased = new List<Kangelane>();

    // Loeb kangelased failist, loob objektid, lisab listi
    static void LoeKangelasedFailist(string failinimi)
    {
        // Otsime faili programmi käivitusmapist (bin/Debug/...)
        string teerada = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, failinimi);
        string[] read = File.ReadAllLines(teerada);

        foreach (string rida in read)
        {
            if (string.IsNullOrWhiteSpace(rida)) continue;

            // Eraldame nime ja asukoha '/' järgi
            string[] osad = rida.Split('/');
            string nimiOsa = osad[0].Trim();
            string asukoht = osad[1].Trim();

            // * tähistab SuperKangelast
            bool onSuper = nimiOsa.Contains("*");
            string nimi = nimiOsa.Replace("*", "").Trim();

            if (onSuper)
                kangelased.Add(new SuperKangelane(nimi, asukoht));
            else
                kangelased.Add(new Kangelane(nimi, asukoht));
        }

        Console.WriteLine("Loetud " + kangelased.Count + " kangelast failist '" + failinimi + "'.\n");
    }

    static void Main()
    {
        Console.WriteLine("==========================================");
        Console.WriteLine("   KANGELASTE AGENTUURI INFOSUSTEEM");
        Console.WriteLine("==========================================\n");

        // samm 1 Loe kangelased failist
        LoeKangelasedFailist("andmed.txt");

        // Kuva kõik kangelased loeteluna
        Console.WriteLine("Koik registreeritud kangelased:");
        foreach (Kangelane k in kangelased)
            Console.WriteLine("  - " + k.ToString());
        Console.WriteLine();

        // samm 2 Vali vähemalt üks tavakangelane ja üks superkangelane
        Kangelane tavakangelane = null;
        SuperKangelane superkangelane = null;

        foreach (Kangelane k in kangelased)
        {
            if (k is SuperKangelane sk && superkangelane == null)
                superkangelane = sk;
            else if (!(k is SuperKangelane) && tavakangelane == null)
                tavakangelane = k;
        }

        int ohus = 1000;

        // samm 3 ja 4 Kutsu meetodid ja kuva info — TAVAKANGELANE
        Console.WriteLine("==========================================");
        Console.WriteLine("TAVAKANGELANE:");
        Console.WriteLine("==========================================");
        Console.WriteLine("ToString()        : " + tavakangelane.ToString());
        Console.WriteLine("Tervitus()        : " + tavakangelane.Tervitus());
        Console.WriteLine("Vormiriietus()    : " + tavakangelane.Vormiriietus());
        Console.WriteLine("MissiooniStaatus(): " + tavakangelane.MissiooniStaatus());
        Console.WriteLine("Paasta(" + ohus + ")  : " + tavakangelane.Paasta(ohus) + " inimest paasteti");
        Console.WriteLine();

        // samm 3 ja 4 Kutsu meetodid ja kuva info — SUPERKANGELANE
        Console.WriteLine("==========================================");
        Console.WriteLine("SUPERKANGELANE:");
        Console.WriteLine("==========================================");
        Console.WriteLine("ToString()        : " + superkangelane.ToString());
        Console.WriteLine("Tervitus()        : " + superkangelane.Tervitus());
        Console.WriteLine("Vormiriietus()    : " + superkangelane.Vormiriietus());
        Console.WriteLine("MissiooniStaatus(): " + superkangelane.MissiooniStaatus());
        Console.WriteLine("Paasta(" + ohus + ")  : " + superkangelane.Paasta(ohus) + " inimest paasteti");
        Console.WriteLine();

        // Kogustatistika kõigi kangelaste kohta
        Console.WriteLine("==========================================");
        Console.WriteLine("KOGU AGENTUURI STATISTIKA:");
        Console.WriteLine("==========================================");
        int kokku = 0;
        foreach (Kangelane k in kangelased)
        {
            int paastetud = k.Paasta(ohus);
            kokku += paastetud;
            string tyup = (k is SuperKangelane) ? "Super" : "Tava ";
            Console.WriteLine("  " + tyup + " | " + k.Nimi + " -> paastis " + paastetud + " inimest");
        }
        Console.WriteLine("------------------------------------------");
        Console.WriteLine("  KOKKU paastetud: " + kokku + " inimest (olukord: " + ohus + " ohus)");
        Console.WriteLine("==========================================");
    }
}