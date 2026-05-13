using System;
using System.Collections.Generic;
using System.Text;

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
