using System;

namespace ZooShop
{
    /// <summary>
    /// TASE 3 — Papagoi (Psittaciformes).
    /// Lisab: sulestiku värv, õpitud sõnade arv.
    /// </summary>
    public class Papagoi : Lind
    {
        public string SulestikuVärv { get; set; } = "Roheline";
        public int    SõnadeArv     { get; set; } = 0;

        public Papagoi(string nimi, DateTime sünniaeg, Sugu sugu,
                       int näljatase, string tiivaulatus, string sulestikuVärv)
        {
            Nimi           = nimi;
            Sünniaeg       = sünniaeg;
            Sugu           = sugu;
            Näljatase      = näljatase;
            Tiivaulatus    = tiivaulatus;
            SulestikuVärv  = sulestikuVärv;
        }

        public override void Söö()
        {
            Console.WriteLine($"  🌰 {Nimi} sööb seemneid ja puuvilju.");
            Näljatase += 15;
        }

        public void ÕpiSõna(string sõna)
        {
            SõnadeArv++;
            Console.WriteLine($"  🦜 {Nimi} ütles: \"{sõna}\"! (teab {SõnadeArv} sõna)");
        }

        public override string LiigiInfo()
            => base.LiigiInfo() + "\n" +
               $"  Sulestik    : {SulestikuVärv}\n" +
               $"  Sõnad       : {SõnadeArv}";
    }
}
