using System;
using System.Collections.Generic;
using System.Text;

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
