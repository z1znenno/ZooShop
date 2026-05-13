using System;

namespace ZooShop
{
    /// <summary>
    /// TASE 3 — Lõvi (Panthera leo).
    /// Lisab: territoorium, karjajuhi staatus.
    /// </summary>
    public class Lovi : Imetaja
    {
        public string Territoorium  { get; set; } = "Määramata";
        public bool   OnKarjajuht   { get; set; } = false;

        public Lovi(string nimi, DateTime sünniaeg, Sugu sugu,
                    int näljatase, string karvavärvus, string territoorium)
        {
            Nimi          = nimi;
            Sünniaeg      = sünniaeg;
            Sugu          = sugu;
            Näljatase     = näljatase;
            Karvavärvus   = karvavärvus;
            Territoorium  = territoorium;
        }

        public override void Söö()
        {
            Console.WriteLine($"  🥩 {Nimi} sööb liha.");
            Näljatase += 25;
        }

        public void Röögib() => Console.WriteLine($"  🦁 {Nimi}: ROAAR!");

        public override string LiigiInfo()
            => base.LiigiInfo() + "\n" +
               $"  Territoorium : {Territoorium}\n" +
               $"  Karjajuht   : {(OnKarjajuht ? "Jah" : "Ei")}";
    }
}
