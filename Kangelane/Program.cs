using System;
using System.Collections.Generic;
using System.IO;

// klass 1 Kangelane (baasklass)
class Kangelane
{
    // Privaatsed isendiväljad
    private string nimi;
    private string asukoht;

    // Konstruktor
    public Kangelane(string nimi, string asukoht)
    {
        this.nimi = nimi;
        this.asukoht = asukoht;
    }

    // Omadused (get/set)
    public string Nimi
    {
        get { return nimi; }
        set { nimi = value; }
    }

    public string Asukoht
    {
        get { return asukoht; }
        set { asukoht = value; }
    }

    // Tagastab 95% ohus olevatest inimestest (ümardatult)
    public virtual int Paasta(int ohus)
    {
        return (int)Math.Round(ohus * 0.95);
    }

    // Tagastab sõne kangelase riietuse kohta
    public virtual string Vormiriietus()
    {
        return nimi + " kannab lihtsat kangelase kostüümi ja maski.";
    }

    // Isikupärane tervitus
    public virtual string Tervitus()
    {
        return "Tere! Mina olen " + nimi + " ja kaitsen " + asukoht + " linna!";
    }

    // Annab teada, kas kangelane on saadaval
    public virtual string MissiooniStaatus()
    {
        return nimi + " on saadaval ja valmis aitama!";
    }

    // Tagastab kangelase kirjelduse
    public override string ToString()
    {
        return "[Kangelane] Nimi: " + nimi + " | Linn: " + asukoht;
    }
}

// klass 2: SuperKangelane (pärib Kangelane)
class SuperKangelane : Kangelane
{
    // Lisaomadus: osavus vahemikus [1.0, 5.0)
    private double osavus;

    // Konstruktor — osavus määratakse juhuslikult
    public SuperKangelane(string nimi, string asukoht) : base(nimi, asukoht)
    {
        Random rand = new Random();
        // NextDouble() annab [0.0, 1.0) → tulemus on [1.0, 5.0)
        osavus = Math.Round(1.0 + rand.NextDouble() * 4.0, 2);
        if (osavus >= 5.0) osavus = 4.99; // garanteerime ülempiiri
    }

    // Osavuse omadus (ainult get, sest määratakse konstruktoris)
    public double Osavus
    {
        get { return osavus; }
    }

    // Päästab (95 + osavus)% inimesi
    public override int Paasta(int ohus)
    {
        double protsent = (95.0 + osavus) / 100.0;
        return (int)Math.Round(ohus * protsent);
    }

    // Eriline superkangelase kostüüm
    public override string Vormiriietus()
    {
        return Nimi + " kannab säravat superkangelase kostüümi keebi ja logoga!";
    }

    // Efektne tervitus
    public override string Tervitus()
    {
        return "*** " + Nimi + " siin! " + Asukoht + " on minu kaitse all - kurjategijad, olge ettevaatlikud! ***";
    }

    // Superkangelane on sageli juba missioonil
    public override string MissiooniStaatus()
    {
        Random rand = new Random();
        if (rand.Next(2) == 0)
            return Nimi + " on hetkel missioonil - tagastab varsti!";
        else
            return Nimi + " on saadaval ja kõrgendatud valmisolekus!";
    }

    // Lisab osavuse info
    public override string ToString()
    {
        return "[SuperKangelane] Nimi: " + Nimi + " | Linn: " + Asukoht + " | Osavus: " + osavus;
    }
}

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